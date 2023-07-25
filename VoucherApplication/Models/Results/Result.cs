namespace VoucherApplication.Models.Results
{
    public class Result
    {
        public bool success { get; set; }
        public string mesage { get; set; }

        public Result(bool result, string mesage)
        {
            this.success = result;
            this.mesage = mesage;
        }
    }
}
