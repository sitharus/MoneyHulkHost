using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileParsers
{
    public class InvalidQIFException : Exception
    {

    }

    public class UnsupportedQIFTypeExecption : Exception
    {
        public string Type { get; }

        public UnsupportedQIFTypeExecption(string type)
        {
            Type = type;
        }
    }
    public class Transaction
    {
        public DateTime Date { get; internal set; }
        public string Memo { get; internal set; }
        public decimal Amount { get; internal set; }
    }

    public class QIF
    {

        public static async Task<QIF> Parse(Stream input)
        {
            var q = new QIF();
            await q.ParseSteam(input);
            return q;
        }

        public static async Task<QIF> Parse(string input)
        {
            var q = new QIF();
            using (var text = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(input)))
            {
                text.Seek(0, SeekOrigin.Begin);
                await q.ParseSteam(text);
            }
            return q;
        }

        string Type;
        List<Transaction> _Transactions = new List<Transaction>(200);
        Transaction _CurrentTransaction = new Transaction();

        public IReadOnlyCollection<Transaction> Transactions => _Transactions;


        QIF()
        {
        }
        
        async Task ParseSteam(Stream input)
        {
            using (var reader = new StreamReader(input))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (line.Length == 0)
                    {
                        continue; // TODO Find out if this is valid in a real QIF
                    }

                    if (Type == null)
                    {
                        if (line != "!Type:Bank")
                        {
                            throw new UnsupportedQIFTypeExecption(line);
                        }
                        Type = "Bank"; // Only support banks for now.
                        continue;
                    }

                    switch (line[0])
                    {
                        case '^':
                            _Transactions.Add(_CurrentTransaction);
                            _CurrentTransaction = new Transaction();
                            break;
                        case 'D':
                            _CurrentTransaction.Date = DateTime.ParseExact(line.Substring(1), "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case 'M':
                            _CurrentTransaction.Memo = line.Substring(1);
                            break;
                        case 'T':
                            _CurrentTransaction.Amount = decimal.Parse(line.Substring(1).Replace(",", ""));
                            break;
                        default:
                            throw new InvalidQIFException();

                    }
                    
                }
            }
        }


    }
}
