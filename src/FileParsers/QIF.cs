using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileParsers
{
    public class QIF
    {
        class Transaction
        {
            public DateTime Date { get; internal set; }
            public string Memo { get; internal set; }
            public decimal Amount { get; internal set; }
        }

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

        IReadOnlyCollection<Transaction> Transactions => Transactions;


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
                    switch (line[0])
                    {
                        case '!':
                            if (line != "!Type:Bank")
                            {
                                throw new NotImplementedException();
                            }
                            break;
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
                        default: break;

                    }
                    
                }
            }
        }


    }
}
