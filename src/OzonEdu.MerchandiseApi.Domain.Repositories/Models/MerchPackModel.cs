namespace OzonEdu.MerchandiseApi.Domain.Repositories.Models
{
    public class MerchPackModel
    {
        public long Id { get; set; }

        public CSharpCourse.Core.Lib.Enums.MerchType Type { get; set; }

        public long[] Items { get; set; }
    }
}
