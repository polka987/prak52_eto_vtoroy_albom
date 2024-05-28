namespace prak52_eto_vtoroy_albom
{
    class notes
    {
        public string title { get; set; }
        public string description {  get; set; }
        public string date;

        public notes() { }
        public notes(string title, string description, string date)
        {
            this.title = title;
            this.description = description;
            this.date = date;
        }
    }
}
