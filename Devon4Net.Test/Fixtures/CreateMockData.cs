using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Test.Fixtures
{
    public static class CreateMockData
    {
        public static Visitor CreateVisitor()
        {
            return new Visitor()
            {
                Uid = new Guid(),
            };
        }

        public static List<AccessCode> CreateAccessCodeList()
        {
            return new List<AccessCode>()
            {
                new AccessCode()
                {
                    Code="Q001",
                    Createdtime = new TimeSpan(0, 14, 11),
                    Endtime = new TimeSpan(0, 14, 11),
                    Id = 1,
                    StartTime = null,
                    Status = Status_t.notStarted,
                }
            };
        }

        public static Queue CreateQueue()
        {
            return new Queue()
            {
                Closed = false,
                Id = 1,
                Name = "Queue1",
                Started = false,
                
            };
        }
    }
}
