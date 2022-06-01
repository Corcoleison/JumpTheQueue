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
                },
                new AccessCode()
                {
                    Code="Q002",
                    Createdtime = new TimeSpan(0, 14, 11),
                    Endtime = new TimeSpan(0, 14, 11),
                    Id = 2,
                    StartTime = null,
                    Status = Status_t.notStarted,
                }
            };
        }

        public static AccessCode CreateAccessCode(int code)
        {
            return new AccessCode()
            {
                Code = "Q" + code.ToString("000"),
                Createdtime = new TimeSpan(0, 14, 11),
                Endtime = new TimeSpan(0, 14, 11),
                Id = code,
                StartTime = null,
                Status = Status_t.notStarted,
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

        public static List<AccessCode> GetNListOfAC(int n)
        {
            var expectedAccessCodeList = new List<AccessCode>();
            for (int i = 1; i <= n; i++)
            {
                expectedAccessCodeList.Add(new AccessCode()
                {
                    Code = "Q" + i.ToString("000"),
                    Id = i,
                });
            }
            return expectedAccessCodeList;
        }

        public static List<AccessCode> GetFullListOfAC()
        {
            var expectedAccessCodeList = new List<AccessCode>();
            for (int i = 1; i < 1000; i++)
            {
                expectedAccessCodeList.Add(new AccessCode()
                {
                    Code = "Q" + i.ToString("000"),
                    Id = i,
                });
            }
            return expectedAccessCodeList;
        }

        public static Queue GetFullQueue()
        {
            var expectedAccessCodeList = GetFullListOfAC();
            var expectedQueue = CreateMockData.CreateQueue();
            foreach(AccessCode ac in expectedAccessCodeList)
            {
                ac.QueueId = expectedQueue.Id;
            }
            return expectedQueue;
        }

        public static List<AccessCode> GetFullListOfACInQueue()
        {
            var expectedAccessCodeList = GetFullListOfAC();
            var expectedQueue = CreateMockData.CreateQueue();
            var aclist = new List<AccessCode>();
            foreach (AccessCode ac in expectedAccessCodeList)
            {
                ac.QueueId = expectedQueue.Id;
                aclist.Add(ac);
            }
            return aclist;
        }
    }
}
