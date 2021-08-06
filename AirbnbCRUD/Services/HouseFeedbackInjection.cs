using AirbnbCRUD.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbCRUD.Services
{
    public interface IHouseFeedback
    {
        List<HouseFeedback> GetAllHousesFeedbacks();
        List<HouseFeedback> GetHouseFeedbacks(int HouseId);
        HouseFeedback GetHouseFeedback(int HouseFeedbackId);
        HouseFeedback CreateHouseFeedback(HouseFeedback houseFeedback);
        HouseFeedback EditHouseFeedback(HouseFeedback houseFeedback);
        void DeleteHouseFeedback(int HouseFeedbackId);
        void DeleteAllHouseFeedbacksByHouseId(int HouseId);
        void DeleteAllHouseFeedbacksByPersonId(int PersonId);
        bool HouseFeedbackExists(int id);

    }
    public class HouseFeedbackInjection:IHouseFeedback
    {
        private readonly ApplicationContext _context;
        public HouseFeedbackInjection(ApplicationContext context)
        {
            _context = context;
        }

        public HouseFeedback CreateHouseFeedback(HouseFeedback houseFeedback)
        {
            _context.HouseFeedbacks.Add(houseFeedback);
            _context.SaveChanges();
            return houseFeedback;
        }

        public void DeleteAllHouseFeedbacksByHouseId(int HouseId)
        {
            var Feedbacks = GetHouseFeedbacks(HouseId);
            foreach(var feedback in Feedbacks)
            {
                _context.HouseFeedbacks.Remove(feedback);
            }
            _context.SaveChanges();
        }

        public void DeleteAllHouseFeedbacksByPersonId(int PersonId)
        {
            var Feedbacks = _context.HouseFeedbacks.Where(p => p.PersonId == PersonId);
            foreach(var feedback in Feedbacks)
            {
                _context.HouseFeedbacks.Remove(feedback);
            }
            _context.SaveChanges();
        }

        public void DeleteHouseFeedback(int HouseFeedbackId)
        {
            var Feedback = GetHouseFeedback(HouseFeedbackId);
            _context.HouseFeedbacks.Remove(Feedback);
            _context.SaveChanges();
        }

        public HouseFeedback EditHouseFeedback(HouseFeedback houseFeedback)
        {
            _context.Entry(houseFeedback).State = EntityState.Modified;
            _context.SaveChanges();
            return houseFeedback;
        }

        public List<HouseFeedback> GetAllHousesFeedbacks()
        {
            return _context.HouseFeedbacks.ToList();
        }

        public HouseFeedback GetHouseFeedback(int HouseFeedbackId)
        {
            return _context.HouseFeedbacks.Find(HouseFeedbackId);
        }

        public List<HouseFeedback> GetHouseFeedbacks(int HouseId)
        {
            return _context.HouseFeedbacks.Where(a => a.HouseId == HouseId).ToList();
        }

        public bool HouseFeedbackExists(int id)
        {
            return _context.HouseFeedbacks.Any(e => e.HouseFeedbackId == id);
        }
    }
}
