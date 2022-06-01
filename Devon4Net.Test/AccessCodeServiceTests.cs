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
            mockAccessCodeRepository.Setup(repository => repository.GetAccessCode(It.IsAny<Expression<Func<AccessCode, bool>>>()))
                .ReturnsAsync(new List<AccessCode>());
            //Act
            var result = await CreateAccessCodeServiceTest().CreateAccessCode(expectedQueue.Id).ConfigureAwait(false);

            //Assert
            Assert.Equal(expectedAClist[0].Code, result.Code); //Q001 should be the result
        }

        [Fact]
        public async Task CreateSecondAccessCodeOK()
        {
            //Arrange
            var expectedVisitor = CreateMockData.CreateVisitor();
            var expectedQueue = CreateMockData.CreateQueue();
            var expectedAClist = CreateMockData.GetNListOfAC(1);
            var expectedResultAC = CreateMockData.CreateAccessCode(2);
            expectedAClist[0].QueueId = expectedQueue.Id;
            expectedAClist[0].VisitorUid = expectedVisitor.Uid;
            

            mockAccessCodeRepository.Setup(repository => repository.Create(
                It.IsAny<string>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<Status_t>(),
                It.IsAny<Guid>(),
                It.IsAny<int>()
                ))
                .ReturnsAsync(expectedResultAC);
            mockVisitorRepository.Setup(repository => repository.Create(It.IsAny<Guid>()))
               .ReturnsAsync(expectedVisitor);
            
            mockAccessCodeRepository.Setup(repository => repository.GetAccessCode(It.IsAny<Expression<Func<AccessCode, bool>>>()))
                .ReturnsAsync(expectedAClist);
            //Act
            var result = await CreateAccessCodeServiceTest().CreateAccessCode(expectedQueue.Id).ConfigureAwait(false);

            //Assert
            Assert.Equal(expectedResultAC.Code, result.Code); //Q002 should be the result
        }

        [Fact]
        public async Task CreateFirstFromFullCodeOK()
        {
            //Arrange
            var expectedVisitor = CreateMockData.CreateVisitor();
            var expectedQueue = CreateMockData.GetFullQueue();
            var expectedAClist = CreateMockData.GetFullListOfACInQueue();
            var expectedResultAC = CreateMockData.CreateAccessCode(1);
            expectedAClist[0].QueueId = expectedQueue.Id;
            expectedAClist[0].VisitorUid = expectedVisitor.Uid;

            mockAccessCodeRepository.Setup(repository => repository.Create(
                It.IsAny<string>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<Status_t>(),
                It.IsAny<Guid>(),
                It.IsAny<int>()
                ))
                .ReturnsAsync(expectedResultAC);
            mockVisitorRepository.Setup(repository => repository.Create(It.IsAny<Guid>()))
               .ReturnsAsync(expectedVisitor);
            mockAccessCodeRepository.Setup(repository => repository.GetAccessCode(It.IsAny<Expression<Func<AccessCode, bool>>>()))
                .ReturnsAsync(expectedAClist);
            //Act
            var result = await CreateAccessCodeServiceTest().CreateAccessCode(expectedQueue.Id).ConfigureAwait(false);

            //Assert
            Assert.Equal("Q001", result.Code); //Q002 should be the result
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
