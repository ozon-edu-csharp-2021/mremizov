using CSharpCourse.Core.Lib.Enums;

namespace OzonEdu.MerchandiseApi.Http.Contracts
{
    public sealed class GiveOutMerchRequest
    {
        public long EmployeeId { get; set; }

        public MerchType MerchType { get; set; }
    }
}
