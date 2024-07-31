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
            Map(m => m.Name).Convert(args => GetName(args));
            Map(m => m.LastName).Convert(args => GetLastName(args));
            Map(m => m.Zipcode).Convert(args => ExtractZipcode(args));
            Map(m => m.City).Convert(args => ExtractCity(args));
            Map(m => m.Color).Convert(args => GetColor(args));
        }

        private int GetId(ConvertFromStringArgs args)
        {
            return Convert.ToInt32(args.Row.Context.Parser.RawRow);
        }

        private string GetName(ConvertFromStringArgs args) 
        {
            int nameRowIndex = 1;
            if(IsFieldValid(args, nameRowIndex))
            {
                string nameField = args.Row.GetField<string>(nameRowIndex).Trim();
                return nameField;
            }
            return string.Empty;
        }

        private string GetLastName(ConvertFromStringArgs args)
        {
            int lastNameRowIndex = 0;
            if (IsFieldValid(args, lastNameRowIndex))
            {
                string lastNameField = args.Row.GetField<string>(lastNameRowIndex).Trim();
                return lastNameField;
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

        private string GetColor(ConvertFromStringArgs args)
        {
            int colorRowIndex = 3;
            if(IsFieldValid(args, colorRowIndex))
            {
                string colorField = args.Row.GetField<string>(colorRowIndex).Trim();
                return colorField;
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
