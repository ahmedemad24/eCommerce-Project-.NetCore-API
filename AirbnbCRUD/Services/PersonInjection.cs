using AirbnbCRUD.Images;
using AirbnbCRUD.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbCRUD.Services
{
    public interface IPerson
    {
        List<Person> GetAllPeople();
        Person CreatePerson(Person person);
        Person GetPerson(int id);
        Person EditPerson(Person person);
        void DeletePerson(int id);
        bool PersonExists(int id);

    }
    public class PersonInjection:IPerson
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment Environment;

        public PersonInjection(ApplicationContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            this.Environment = _environment;
        }

        public Person CreatePerson(Person person)
        {
            try
            {
                
                _context.People.Add(person);
                _context.SaveChanges();
                if (person.ProfilePicture != null)
                {
                    string[] photoName = person.ProfilePicture.FileName.Split('.');
                    var imageName = $"{person.PersonId}.{photoName[photoName.Length - 1]}";
                    string uploaded = Path.Combine("Images", "ProfilePicturePerson");
                    string filpath = Path.Combine(uploaded, imageName);

                    var filePath = $"~Images\\ProfilePicturePerson\\{imageName}";
                    var profilePic = ImageStuff.HandleImage(person.ProfilePicture);
                    using var imageMemory = new MemoryStream();
                    profilePic.Write(imageName);
                    File.Move(imageName, filpath);
                    person.ProfilePictureName = filpath;
                }
                
                _context.SaveChanges();
                return person;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public void DeletePerson(int id)
        {
            var person = GetPerson(id);
            var personFeedbacksAsHost = _context.PersonFeedbacks.Where(p => p.PersonHostId == id);
            var personFeedbacksAsCustomer = _context.PersonFeedbacks.Where(p => p.PersonCustomerId == id);
            var bookings = _context.Bookings.Where(p => p.PersonId == id);
            var houses = _context.Houses.Where(p => p.PersonId == id);
            if(personFeedbacksAsHost != null)
            {
                foreach(var personFeedbackAsHost in personFeedbacksAsHost)
                {
                    _context.PersonFeedbacks.Remove(personFeedbackAsHost);
                }
            }
            if(personFeedbacksAsCustomer != null)
            {
                foreach(var personFeedbackAsCustomer in personFeedbacksAsCustomer)
                {
                    _context.PersonFeedbacks.Remove(personFeedbackAsCustomer);
                }
            }
            if (bookings != null)
            {
                foreach(var booking in bookings)
                {
                    _context.Bookings.Remove(booking);
                }
            }
            if (houses != null)
            {
                foreach(var house in houses)
                {
                    var housePhotos = _context.HousePhotos.Where(h => h.HouseId == house.HouseId);
                    if(housePhotos != null)
                    {
                        foreach(var housePhoto in housePhotos)
                        {
                            _context.HousePhotos.Remove(housePhoto);
                        }
                    }
                    _context.Houses.Remove(house);
                }
            }
            var filpath = $"~Images\\ProfilePicturePerson\\{person.PersonId}";
            if (File.Exists(filpath))
                File.Delete(filpath);
            _context.People.Remove(person);
            _context.SaveChanges();
        }

        public Person EditPerson(Person person)
        {
            if (person.ProfilePicture != null)
            {
                string[] photoName = person.ProfilePicture.FileName.Split('.');
                var imageName = $"{person.PersonId}.{photoName[photoName.Length - 1]}";
                string uploaded = Path.Combine("Images", "ProfilePicturePerson");
                string filpath = Path.Combine(uploaded, imageName);

                var profilePic = ImageStuff.HandleImage(person.ProfilePicture);
                using var imageMemory = new MemoryStream();
                profilePic.Write(imageName);

                if(File.Exists(filpath))
                    File.Delete(filpath);

                File.Move(imageName, filpath);
                person.ProfilePictureName = filpath;
            }
            _context.Entry(person).State = EntityState.Modified;
            _context.SaveChanges();
            return person;
        }

        public List<Person> GetAllPeople()
        {
            return _context.People.ToList();
        }

        public Person GetPerson(int id)
        {
            return _context.People.Find(id);
        }

        public bool PersonExists(int id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }
    }
}
