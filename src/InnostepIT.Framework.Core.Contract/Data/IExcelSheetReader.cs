namespace InnostepIT.Framework.Core.Contract.Data;

public interface IExcelSheetReader<T> where T : class
{
    ICollection<T> GetRecords(string filepath, string sheetName, Type classMapType);
}