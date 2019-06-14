namespace ATG.DAL.DatabaseModels
{
    public class ATGVisitor
    {
        public int VisitorID { get; set; }
        public string IPAddress { get; set; }
        public string OS { get; set; }
        public string Browser { get; set; }
        public int? SexID { get; set; }
        public int malePercent { get; set; }
        public int femalePercent { get; set; }
    }
}
