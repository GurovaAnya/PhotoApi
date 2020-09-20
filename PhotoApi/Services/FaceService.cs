using Microsoft.EntityFrameworkCore;
using PhotoApi.Exceptions;
using PhotoApi.Models;
using PhotoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoApi.Services.Interfaces;

namespace PhotoApi.Services
{
    public class FaceService : IFaceService
    {
        private readonly PhotoDbContext _context;
        private readonly IStorageAccessingService _googleStorageService;

        public FaceService(PhotoDbContext context, IStorageAccessingService googleStorageService)
        {
            _context = context;
            _googleStorageService = googleStorageService;
        }

        public async Task<IEnumerable<FaceViewModel>> GetFaces(int personId)
        {
            var person = await _context.People.Where(p => p.Id == personId).SingleAsync();
            if (person == null)
                throw new BadRequestException("Не указан идентификатор пользователя");
            var faces = await _context.Faces.Where(f => f.PersonId == personId).ToListAsync();
            var faceViewModels = new List<FaceViewModel>();
            foreach (var face in faces)
                faceViewModels.Add(await MapToViewModel(face, _googleStorageService));
            return faceViewModels;
        }


        public async Task<FaceViewModel> GetFace(int id, int personId)
        {
            var face = await _context.Faces.Where(f => f.PersonId == personId && f.Id == id).SingleAsync();
            if (face == null)
            {
                throw new NotFoundException("Лица с такими параметрами не найдено");
            }

            // Получение фото из облака
            var photo = await _googleStorageService.Read(face.PhotoName);

            var faceViewModel = new FaceViewModel() { Id = face.Id, PersonId = personId, Photo = photo };
            return faceViewModel;
        }


        public async Task PutFace(int id, FaceViewModel faceViewModel, int personId)
        {
            if (id != faceViewModel.Id)
            {
                throw new BadRequestException("Данные запроса не совпадают");
            }

            var face = await _context.Faces.Where(f => f.Id == id).SingleAsync();

            if (personId != face.PersonId)
            {
                throw new BadRequestException("Данные запроса не совпадают");
            }

            face.PersonId = faceViewModel.PersonId;


            int photoHash = Face.CreateHash(faceViewModel.Photo);
            string oldName = null;
            // Если фото поменялось
            if (photoHash != face.PhotoHash)
            {
                oldName = face.PhotoName;
                face.PhotoName = DateTime.UtcNow.Ticks.ToString();
                face.PhotoHash = photoHash;
            }

            _context.Entry(face).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                if (oldName != null)
                {
                    await _googleStorageService.Delete(oldName);
                    await _googleStorageService.Write(faceViewModel.Photo, face.PhotoName);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaceExists(id))
                {
                    throw new NotFoundException("Лица с такими параметрами не найдено");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<FaceViewModel> PostFace(FaceViewModel faceViewModel, int personId)
        {
            if (personId != faceViewModel.PersonId)
            {
                throw new BadRequestException("Данные запроса не совпадают");
            }

            string path = DateTime.UtcNow.Ticks.ToString();
            await _googleStorageService.Write(faceViewModel.Photo, path);
            var face = MapToModel(faceViewModel, path);
            _context.Faces.Add(face);

            await _context.SaveChangesAsync();
            faceViewModel.Id = face.Id;
            return faceViewModel;
        }


        public async Task<FaceViewModel> DeleteFace(int id, int personId)
        {
            var face = await _context.Faces.FindAsync(id);
            if (face == null)
            {
                throw new NotFoundException("Лица с такими параметрами не найдено");
            }

            if (face.PersonId != personId)
            {
                throw new BadRequestException("Данные запроса не совпадают");
            }

            var photo = await _googleStorageService.Read(face.PhotoName);
            _context.Faces.Remove(face);
            await _context.SaveChangesAsync();
            await _googleStorageService.Delete(face.PhotoName);

            return new FaceViewModel() { Id = face.Id, PersonId = face.PersonId, Photo = photo };
        }

        private bool FaceExists(int id)
        {
            return _context.Faces.Any(e => e.Id == id);
        }

        private static async Task<FaceViewModel> MapToViewModel(Face face, IStorageAccessingService googleStorageService)
        {
            return new FaceViewModel
            {
                Id = face.Id,
                Photo = await googleStorageService.Read(face.PhotoName),
                PersonId = face.PersonId
            };
        }

        private static Face MapToModel(FaceViewModel faceViewModel, string path)
        {
            return new Face()
            {
                Id = faceViewModel.Id,
                PersonId = faceViewModel.PersonId,
                PhotoHash = Face.CreateHash(faceViewModel.Photo),
                PhotoName = path
            };
        }
    }
}
