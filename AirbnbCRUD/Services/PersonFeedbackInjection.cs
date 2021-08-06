using AirbnbCRUD.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbCRUD.Services
{
    public interface IPersonFeedback
    {
        List<PersonFeedback> GetAllPeopleFeedbacks();
        List<PersonFeedback> GetAllPeopleFeedbacksByHostId(int PersonHostId);
        List<PersonFeedback> GetAllPeopleFeedbacksByCustomerId(int PersonCustomerId);
        PersonFeedback CreatePersonFeedback(PersonFeedback feedback);
        PersonFeedback GetPersonFeedback(int PersonFeedbackId);
        PersonFeedback EditPersonFeedback(PersonFeedback feedback);
        void DeletePersonFeedback(int PersonFeedbackId);
        void DeleteAllPeopleFeedbacksByHostId(int PersonHostId);
        void DeleteAllPeopleFeedbacksByCustomerId(int PersonCustomerId);
        bool PersonFeedbackExists(int id);
    }
    public class PersonFeedbackInjection:IPersonFeedback
    {
        private readonly ApplicationContext _context;
        public PersonFeedbackInjection(ApplicationContext context)
        {
            _context = context;
        }

        public PersonFeedback CreatePersonFeedback(PersonFeedback feedback)
        {
            _context.PersonFeedbacks.Add(feedback);
            _context.SaveChanges();
            return feedback;
        }

        public void DeleteAllPeopleFeedbacksByCustomerId(int PersonCustomerId)
        {
            var feedbacks = GetAllPeopleFeedbacksByCustomerId(PersonCustomerId);
            foreach(var feedback in feedbacks)
            {
                _context.PersonFeedbacks.Remove(feedback);
            }
            _context.SaveChanges();
        }

        public void DeleteAllPeopleFeedbacksByHostId(int PersonHostId)
        {
            var feedbacks = GetAllPeopleFeedbacksByHostId(PersonHostId);
            foreach(var feedback in feedbacks)
            {
                _context.PersonFeedbacks.Remove(feedback);
            }
            _context.SaveChanges();
        }

        public void DeletePersonFeedback(int PersonFeedbackId)
        {
            var feedback = GetPersonFeedback(PersonFeedbackId);
            _context.PersonFeedbacks.Remove(feedback);
            _context.SaveChanges();
        }

        public PersonFeedback EditPersonFeedback(PersonFeedback feedback)
        {
            _context.Entry(feedback).State = EntityState.Modified;
            _context.SaveChanges();
            return feedback;
        }

        public List<PersonFeedback> GetAllPeopleFeedbacks()
        {
            return _context.PersonFeedbacks.ToList();
        }

        public List<PersonFeedback> GetAllPeopleFeedbacksByCustomerId(int PersonCustomerId)
        {
            var feedbacks = _context.PersonFeedbacks.Where(a => a.PersonCustomerId == PersonCustomerId);
            return feedbacks.ToList();
        }

        public List<PersonFeedback> GetAllPeopleFeedbacksByHostId(int PersonHostId)
        {
            var feedbacks = _context.PersonFeedbacks.Where(a => a.PersonHostId == PersonHostId);
            return feedbacks.ToList();
        }

        public PersonFeedback GetPersonFeedback(int PersonFeedbackId)
        {
            return _context.PersonFeedbacks.Find(PersonFeedbackId);
        }
        public bool PersonFeedbackExists(int id)
        {
            return _context.PersonFeedbacks.Any(e => e.PersonFeedbackId == id);
        }
    }
}
