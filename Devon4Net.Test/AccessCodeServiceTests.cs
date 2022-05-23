using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Test.Fixtures;
using Devon4Net.WebAPI.Implementation;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Service;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Devon4Net.Test
{
    public class AccessCodeServiceTests : Base
    {
        [Fact]
        public async Task GetAccessCodeOKAsync()
        {
            //Arrange
            var expectedAccessCodeList = new List<AccessCode>()
            {
                new AccessCode()
                {
                    Code="Q001",
                }
            };

            mockAccessCodeRepository.Setup(repository => repository.GetAccessCode(It.IsAny<Expression<Func<AccessCode, bool>>>()))
                .ReturnsAsync(expectedAccessCodeList);
            //Act
            var result = await CreateAccessCodeServiceTest().GetAccessCode().ConfigureAwait(false);

            //Assert
            List<AccessCodeDto> resultList = result.ToList();
            Assert.Equal(expectedAccessCodeList[0].Id, resultList[0].Id);
        }

        public List<AccessCode> GetQueueWith()
        {
            var expectedAccessCodeList = new List<AccessCode>();
            for(int i=0; i<100; i++)
            {
                expectedAccessCodeList.Add(new AccessCode()
                {
                }
                );
            };
            return expectedAccessCodeList;
        }

        [Fact]
        public async Task CreateFirstAccessCodeOK()
        {
            //Arrange
            var expectedVisitor = CreateMockData.CreateVisitor();
            var expectedQueue = CreateMockData.CreateQueue();
            var expectedAClist = CreateMockData.CreateAccessCodeList();

            mockAccessCodeRepository.Setup(repository => repository.Create(
                It.IsAny<string>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<Status_t>(),
                It.IsAny<Guid>(),
                It.IsAny<int>()
                ))
                .ReturnsAsync(expectedAClist[0]);
            mockVisitorRepository.Setup(repository => repository.Create(It.IsAny<Guid>()))
               .ReturnsAsync(expectedVisitor);
            //Act
            var result = await CreateAccessCodeServiceTest().CreateAccessCode(expectedQueue.Id).ConfigureAwait(false);

            //Assert
            Assert.Equal(expectedAClist[0].Id, result.Id); //Q001 should be the result
        }
    }

    public class Base
    {
        public readonly Mock<IAccessCodeRepository> mockAccessCodeRepository = new Mock<IAccessCodeRepository>();
        public readonly Mock<IQueueRepository> mockQueueRepository = new Mock<IQueueRepository>();
        public readonly Mock<IVisitorRepository> mockVisitorRepository = new Mock<IVisitorRepository>();

        public AccessCodeService CreateAccessCodeServiceTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork<jumpthequeueContext>>();

            mockUnitOfWork.Setup(x => x.Repository<IAccessCodeRepository>())
                       .Returns(mockAccessCodeRepository.Object);
            mockUnitOfWork.Setup(x => x.Repository<IQueueRepository>())
                       .Returns(mockQueueRepository.Object);
            mockUnitOfWork.Setup(x => x.Repository<IVisitorRepository>())
                       .Returns(mockVisitorRepository.Object);

            return new AccessCodeService(mockUnitOfWork.Object);
        }
    }
}
