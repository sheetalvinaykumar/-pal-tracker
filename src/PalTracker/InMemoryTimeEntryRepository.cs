using System.Collections.Generic;

namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
        private List<TimeEntry> timeEntries;
        public InMemoryTimeEntryRepository()
        {
             timeEntries  = new List<TimeEntry>();
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            int index = timeEntries.Count + 1;

            TimeEntry newTimeEntry = new TimeEntry(index,timeEntry.ProjectId, timeEntry.UserId,timeEntry.Date, timeEntry.Hours );
            
            timeEntries.Add(newTimeEntry);            

            return newTimeEntry;
        } 


        public TimeEntry Update(long firstValue, TimeEntry timeEntry)
        {

            timeEntry.Id = firstValue;
        
           int index = timeEntries.FindIndex(te => te.Id == firstValue);

           timeEntries[index] =timeEntry;
            
           return timeEntry;
        }

        public void Delete(long v)
        {
           timeEntries.RemoveAll(T=>T.Id == v);
        }

        public TimeEntry Find(long firstValue)
        {
            return timeEntries.Find(te => te.Id == firstValue);
        }
        public bool Contains(long firstValue)
        {
            return timeEntries.Exists(te => te.Id == firstValue);
        }
        public IEnumerable<TimeEntry> List()
        {
            return timeEntries;
        }

    }
}