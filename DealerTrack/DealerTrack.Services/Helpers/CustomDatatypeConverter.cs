using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrack.Helpers
{
    public class CustomDatatypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (memberMapData.Names[0].Equals("Price"))
            {
                if (Decimal.TryParse(text.Replace(",", ""), out decimal price))
                    return price;
                else
                    return 0.0m;
            }
            else
            {
                string[] dateTypeArr = { "MM/dd/yyyy", "M/d/yyyy" };
                if (DateTime.TryParseExact(text, dateTypeArr, new CultureInfo("en-US"), DateTimeStyles.None, out DateTime firstDateValue))
                    return firstDateValue;
                else
                    return null;
            }
        }
    }
}
