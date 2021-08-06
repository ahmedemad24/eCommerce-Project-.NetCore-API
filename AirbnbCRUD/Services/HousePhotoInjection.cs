using AirbnbCRUD.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbCRUD.Services
{
    public interface IHousePhoto
    {
        IEnumerable<HousePhoto> GetAllHousePhotos();
        IEnumerable<HousePhoto> GetHousePhotos(int id);
        HousePhoto GetHousePhoto(string HousePhoto);
        HousePhoto CreateHousePhoto(HousePhoto housePhoto);
        HousePhoto EditHousephoto(HousePhoto housePhoto);
        void DeleteHousePhoto(string HousePhoto);
        void DeleteHousePhotos(int HouseId);
        bool HousePhotoExists(int id,string housePhotoNumber);
    }
    public class HousePhotoInjection:IHousePhoto
    {
        private readonly ApplicationContext _context;
        public HousePhotoInjection(ApplicationContext context)
        {
            _context = context;
        }

        public HousePhoto CreateHousePhoto(HousePhoto housePhoto)
        {
            _context.HousePhotos.Add(housePhoto);
            _context.SaveChanges();
            return housePhoto;
        }

        public void DeleteHousePhoto( string HousePhoto)
        {
            var photo = GetHousePhoto( HousePhoto);
            _context.HousePhotos.Remove(photo);
            _context.SaveChanges();
        }

        public void DeleteHousePhotos(int HouseId)
        {
            var photos = GetHousePhotos(HouseId);
            foreach(var photo in photos)
            {
                _context.HousePhotos.Remove(photo);
            }
            _context.SaveChanges();
        }

        public HousePhoto EditHousephoto(HousePhoto housePhoto)
        {
            _context.Entry(housePhoto).State = EntityState.Modified;
            _context.SaveChanges();
            return housePhoto;
        }

        public IEnumerable<HousePhoto> GetAllHousePhotos()
        {
            return _context.HousePhotos.ToArray();
        }

        public HousePhoto GetHousePhoto(string HousePhoto)
        {
            return _context.HousePhotos.FirstOrDefault(a => a.HousePhotos == HousePhoto);
        }

        public IEnumerable<HousePhoto> GetHousePhotos(int id)
        {
            return _context.HousePhotos.Where(a => a.HouseId == id).ToArray();
        }

        public bool HousePhotoExists(int id, string housePhotoNumber)
        {
            return _context.HousePhotos.Any(e => e.HouseId == id && e.HousePhotos==housePhotoNumber);
        }
    }
}
