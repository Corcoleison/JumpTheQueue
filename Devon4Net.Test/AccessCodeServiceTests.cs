using Devon4Net.Domain.UnitOfWork.UnitOfWork;
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
