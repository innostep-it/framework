using System.Globalization;
using CsvHelper;
using CsvHelper.Excel;
using InnostepIT.Framework.Core.Contract.Data;
using Microsoft.Extensions.Logging;

namespace InnostepIT.Framework.Core.Data;

public class ExcelSheetReader<T> : IExcelSheetReader<T> where T : class
{
    private readonly ILogger<ExcelSheetReader<T>> _logger;

    public ExcelSheetReader(ILogger<ExcelSheetReader<T>> logger)
    {
        _logger = logger;
    }

    public ICollection<T> GetRecords(string filepath, string sheetName, Type classMapType)
    {
        using var parser = new ExcelParser(filepath, sheetName, CultureInfo.InvariantCulture);
        using var reader = new CsvReader(parser);

        parser.Context.RegisterClassMap(classMapType);
        var records = reader.GetRecords<T>().ToList();

        _logger.LogDebug("Read {RecordsCount} entries of {Type} from sheet", records.Count, nameof(T));

        return records;
    }
}