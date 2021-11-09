using System;
using FluentAssertions;
using NUnit.Framework;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;

namespace OzonEdu.MerchandiseApi.Domain.Tests.Entities
{
    public class MerchTests
    {
        [TestCase(MerchStatusEnum.New, TestName = "Can move to 'Waiting' status from 'New'")]
        [TestCase(MerchStatusEnum.Waiting, TestName = "Can move to 'Waiting' status from 'Waiting'")]
        public void CanMoveToWaitingStatus(MerchStatusEnum status)
        {
            var merch = TestData.CreateMerch(status);

            merch.Waiting();

            merch.Status.Should().Be(MerchStatus.Waiting);
        }

        [TestCase(MerchStatusEnum.Done, TestName = "Can not move to 'Waiting' status from 'Done'")]
        public void CanNotMoveToWaitingStatus(MerchStatusEnum status)
        {
            var merch = TestData.CreateMerch(status);

            Action action = () => merch.Waiting();
            action.Should().Throw<MerchInvalidStatusException>();
        }

        [TestCase(MerchStatusEnum.New, TestName = "Can move to 'Done' status from 'New'")]
        [TestCase(MerchStatusEnum.Waiting, TestName = "Can move to 'Done' status from 'Waiting'")]
        public void CanMoveToDoneStatus(MerchStatusEnum status)
        {
            var merch = TestData.CreateMerch(status);

            merch.Done();

            merch.Status.Should().Be(MerchStatus.Done);
        }
    }
}