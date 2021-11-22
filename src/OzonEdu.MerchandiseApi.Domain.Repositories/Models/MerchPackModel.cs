namespace OzonEdu.MerchandiseApi.Domain.Repositories.Models
{
    public sealed class MerchPackModel
    {
        public long Id { get; set; }

        public CSharpCourse.Core.Lib.Enums.MerchType Type { get; set; }

        public string[] Items { get; set; }
    }
}
