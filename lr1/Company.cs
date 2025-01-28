namespace lr_one
{
    public class Company
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string[] Countries { get; set; }
        public int Staff { get; set; }
        public Company()
        {
            this.Name = "";
            this.Year = 0;
            this.Countries = [];
            this.Staff = 0;
        }
        public override string ToString()
        {
            return $"Info about object \nCompany name: {this.Name} \nCompany year: {this.Year} \nСompany branches: {string.Join(", ", this.Countries)} \nCompany staff: {this.Staff} people \n";
        }
    }
}
