using CsvHelper;
using CsvHelper.Configuration;
using PersonRESTful.Services;

namespace PersonRESTful.Models
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Id).Convert(args => GetId(args));
            Map(m => m.Name).Convert(args => getName(args));
            Map(m => m.LastName).Index(1).Convert(args => args.Row.GetField<string>(0)?.Trim()).Validate(args => !string.IsNullOrEmpty(args.Field)); ;
            Map(m => m.Zipcode).Convert(args => ExtractZipcode(args));
            Map(m => m.City).Convert(args => ExtractCity(args));
            Map(m => m.Color).Index(3);
        }

        private int GetId(ConvertFromStringArgs args)
        {
            return Convert.ToInt32(args.Row.Context.Parser.RawRow);
        }

        private string getName(ConvertFromStringArgs args) 
        {
            int nameRowIndex = 1;
            if(IsFieldValid(args, nameRowIndex))
            {
                string nameField = args.Row.GetField<string>(nameRowIndex).Trim();
                return nameField;
            }
            return string.Empty;
        }

        private string ExtractZipcode(ConvertFromStringArgs args)
        {
            int zipcodeRowIndex = 2;
            int zipcodeLength = 5;

            if(IsFieldValid(args, zipcodeRowIndex))
            {
                string zipcodeField = args.Row.GetField<string>(zipcodeRowIndex).Trim().Substring(0, zipcodeLength);
                return zipcodeField;
            }
            return string.Empty;   
        }

        private string ExtractCity(ConvertFromStringArgs args)
        {

            int cityRowIndex = 2;
            int cityFieldIndex = 6;

            if(IsFieldValid(args, cityRowIndex))
            {
                string cityField = args.Row.GetField<string>(cityRowIndex).Trim().Substring(cityFieldIndex);
                return cityField;
            }

            return string.Empty;
        }

        private bool IsFieldValid(ConvertFromStringArgs args, int fieldIndexInRow)
        {
            int numberOfFieldsPerRow = args.Row.Parser.Count;


            if (numberOfFieldsPerRow > fieldIndexInRow)
            {
                string field = args.Row.GetField<string>(fieldIndexInRow).Trim();

                if (!string.IsNullOrEmpty(field))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
