using AirbnbCRUD.Images;
using AirbnbCRUD.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbCRUD.Services
{
    public interface IHouse
    {
        IEnumerable<House> GetAllHouses();
        IEnumerable<House> GetAllHousesInCity(string city);
        IEnumerable<House> GetAllHousesByPersonId(int id);
        House GetHouse(int id);
        House CreateHouse(House house);
        House EditHouse(House house);
        void DeleteHouse(int id);
        bool HouseExists(int id);
    }
    public class HouseInjection:IHouse
    {
        private readonly ApplicationContext _context;
        private readonly IHostEnvironment _environment;


        public HouseInjection(ApplicationContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        public House CreateHouse(House house)
        {
            
            _context.Houses.Add(house);
            _context.SaveChanges();

            if (house.HousePhotoFiles != null)
            {
                try
                {

                    string[] files = Directory.GetFiles($"Images\\HouseImages\\{house.HouseId}");
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                        }
                        Directory.Delete($"Images\\HouseImages\\{house.HouseId}");
                    }
                }
                catch
                {

                }
                
                var RelatedPath = $"Images\\HouseImages\\{house.HouseId}";
                DirectoryInfo di = Directory.CreateDirectory(RelatedPath);
                for (var i = 0; i < house.HousePhotoFiles.Length; i++)
                {
                    var houseFile = house.HousePhotoFiles[i];
                    var houseName = houseFile.FileName.Split('.');
                    var newHouseName = $"{house.HouseId}-{i}.{houseName[houseName.Length - 1]}";
                    var Path = $"{di.FullName}\\{newHouseName}";
                    var Photo = ImageStuff.HandleImage(houseFile);
                    Photo.Write(newHouseName);
                    File.Move(newHouseName, Path);
                    house.HousePhotoName = RelatedPath + "\\" + newHouseName;
                    _context.HousePhotos.Add(new HousePhoto() { HouseId = house.HouseId, HousePhotos = RelatedPath + "\\" + newHouseName });
                }
            }
            _context.SaveChanges();
            return house;
        }

        public void DeleteHouse(int id)
        {
            var House = GetHouse(id);
            var HousePhotos = _context.HousePhotos.Where(h => h.HouseId == id);
            var HouseFeedbacks = _context.HouseFeedbacks.Where(h => h.HouseId == id);
            var HouseBookings = _context.Bookings.Where(h => h.HouseId == id);
            if (HousePhotos != null)
            {
                foreach(var housePhoto in HousePhotos)
                {
                    var Path = housePhoto.HousePhotos;
                    File.Delete(Path);
                    _context.HousePhotos.Remove(housePhoto);
                }
                Directory.Delete($"Images\\HouseImages\\{House.HouseId}");
            }
            if(HouseFeedbacks != null)
            {
                foreach(var houseFeedback in HouseFeedbacks)
                {
                    _context.HouseFeedbacks.Remove(houseFeedback);
                }
            }
            if (HouseBookings != null)
            {
                foreach(var houseBooking in HouseBookings)
                {
                    _context.Bookings.Remove(houseBooking);
                }
            }
            _context.Houses.Remove(House);
            _context.SaveChanges();
        }

        public House EditHouse(House house)
        {
            try
            {

                if (house.HousePhotoFiles != null)
                {
                    string[] files = Directory.GetFiles($"Images\\HouseImages\\{house.HouseId}");
                    foreach(var file in files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                    Directory.Delete($"Images\\HouseImages\\{house.HouseId}");
                    DirectoryInfo di = Directory.CreateDirectory($"Images\\HouseImages\\{house.HouseId}");

                    for (var i = 0; i < house.HousePhotoFiles.Length; i++)
                    {
                        var houseFile = house.HousePhotoFiles[i];
                        var houseName = houseFile.FileName.Split('.');
                        var newHouseName = $"{house.HouseId}-{i}.{houseName[houseName.Length - 1]}";
                        var Path = $"{di.FullName}\\{newHouseName}";
                        var Photo = ImageStuff.HandleImage(houseFile);
                        Photo.Write(newHouseName);
                        File.Move(newHouseName, Path);
                        _context.HousePhotos.Add(new HousePhoto() { HouseId = house.HouseId, HousePhotos = Path });
                    }
                    
                }
                _context.Entry(house).State = EntityState.Modified;
                _context.SaveChanges();
                return house;
            }catch( Exception e)
            {
                throw;
            }
        }

        public IEnumerable<House> GetAllHouses()
        {
            return _context.Houses.ToArray();
        }

        public IEnumerable<House> GetAllHousesByPersonId(int id)
        {
            var houses = GetAllHouses().Where(x => x.PersonId == id).ToArray();
            return houses;
        }

        public IEnumerable<House> GetAllHousesInCity(string city)
        {
            return _context.Houses.Where(h => EF.Functions.Like(h.HouseCity, $"%{city}%")).ToArray();
        }

        public House GetHouse(int id)
        {
            return _context.Houses.Find(id);
        }

        public bool HouseExists(int id)
        {
            return _context.Houses.Any(e => e.HouseId == id);
        }
    }
}
