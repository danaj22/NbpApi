using CurrencyAppApi.Entities;
using CurrencyAppApi.Services;
using Hangfire;

namespace CurrencyAppApi
{
    public class CurrencySeeder
    {
        private readonly CurrencyDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IExchangeRatesJob _fetchExchangeRatesJob;
        private readonly IHistoricalExchangeRatesJob _historicalExchangeRatesJob;

        public CurrencySeeder(CurrencyDbContext dbContext, 
            IConfiguration configuration, 
            IBackgroundJobClient backgroundJobClient, 
            IExchangeRatesJob fetchExchangeRatesJob, 
            IHistoricalExchangeRatesJob historicalExchangeRatesJob)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _backgroundJobClient = backgroundJobClient;
            _fetchExchangeRatesJob = fetchExchangeRatesJob;
            _historicalExchangeRatesJob = historicalExchangeRatesJob;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Currencies.Any())
                {
                    _backgroundJobClient.Enqueue(() => AddCurrencies());
                }

                if (!_dbContext.Sources.Any())
                {
                    _backgroundJobClient.Enqueue(() => AddSources());
                }

                if (!_dbContext.ExchangeRates.Any())
                {
                    _backgroundJobClient.Enqueue(() => _fetchExchangeRatesJob.FetchData());
                }
                else
                {
                    _backgroundJobClient.Enqueue(() => _fetchExchangeRatesJob.UpdateData());
                }

                if (!_dbContext.HistoricalExchangeRates.Any())
                {
                    _backgroundJobClient.Enqueue(() => _historicalExchangeRatesJob.FetchData());
                }
                else
                {
                    _backgroundJobClient.Enqueue(() => _historicalExchangeRatesJob.CompleteData());
                }
            }
        }

        public async Task AddCurrencies()
        {
            var currencies = GetCurrencies();
            await _dbContext.AddRangeAsync(currencies);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddSources()
        {
            var sources = GetSources();
            await _dbContext.AddRangeAsync(sources);
            await _dbContext.SaveChangesAsync();
        }

        private static IEnumerable<Currency> GetCurrencies()
        {
            return 
                [
                new() {
                  Id = "PLN",
                  Name = "złoty"
                },
                new() {
                  Name = "bat (Tajlandia)",
                  Id = "THB",
                },
                new() {
                  Name = "dolar amerykański",
                  Id = "USD",
                },
                new() {
                  Name = "dolar australijski",
                  Id = "AUD",
                },
                new() {
                  Name = "dolar Hongkongu",
                  Id = "HKD",
                },
                new() {
                  Name = "dolar kanadyjski",
                  Id = "CAD",
                },
                new() {
                  Name = "dolar nowozelandzki",
                  Id = "NZD",
                },
                new() {
                  Name = "dolar singapurski",
                  Id = "SGD",
                },
                new() {
                  Name = "euro",
                  Id = "EUR",
                },
                new() {
                  Name = "forint (Węgry)",
                  Id = "HUF",
                },
                new() {
                  Name = "frank szwajcarski",
                  Id = "CHF",
                },
                new() {
                  Name = "funt szterling",
                  Id = "GBP",
                },
                new() {
                  Name = "hrywna (Ukraina)",
                  Id = "UAH",
                },
                new() {
                  Name = "jen (Japonia)",
                  Id = "JPY",
                },
                new() {
                  Name = "korona czeska",
                  Id = "CZK",
                },
                new() {
                  Name = "korona duńska",
                  Id = "DKK",
                },
                new() {
                  Name = "korona islandzka",
                  Id = "ISK",
                },
                new() {
                  Name = "korona norweska",
                  Id = "NOK",
                },
                new() {
                  Name = "korona szwedzka",
                  Id = "SEK",
                },
                new() {
                  Name = "lej rumuński",
                  Id = "RON",
                },
                new() {
                  Name = "lew (Bułgaria)",
                  Id = "BGN",
                },
                new() {
                  Name = "lira turecka",
                  Id = "TRY",
                },
                new() {
                  Name = "nowy izraelski szekel",
                  Id = "ILS",
                },
                new() {
                  Name = "peso chilijskie",
                  Id = "CLP",
                },
                new() {
                  Name = "peso filipińskie",
                  Id = "PHP",
                },
                new() {
                  Name = "peso meksykańskie",
                  Id = "MXN",
                },
                new() {
                  Name = "rand (Republika Południowej Afryki)",
                  Id = "ZAR",
                },
                new() {
                  Name = "real (Brazylia)",
                  Id = "BRL",
                },
                new() {
                  Name = "ringgit (Malezja)",
                  Id = "MYR",
                },
                new() {
                  Name = "rupia indonezyjska",
                  Id = "IDR",
                },
                new() {
                  Name = "rupia indyjska",
                  Id = "INR",
                },
                new() {
                  Name = "won południowokoreański",
                  Id = "KRW",
                },
                new() {
                  Name = "yuan renminbi (Chiny)",
                  Id = "CNY",
                },
                new() {
                  Name = "SDR (MFW)",
                  Id = "XDR",
                },
                new() {
                  Name = "afgani (Afganistan)",
                  Id = "AFN",

                },
                new() {
                  Name = "ariary (Madagaskar)",
                  Id = "MGA",

                },
                new() {
                  Name = "balboa (Panama)",
                  Id = "PAB",

                },
                new() {
                  Name = "birr etiopski",
                  Id = "ETB",

                },
                new() {
                  Name = "boliwar soberano (Wenezuela)",
                  Id = "VES",

                },
                new() {
                  Name = "boliwiano (Boliwia)",
                  Id = "BOB",

                },
                new() {
                  Name = "colon kostarykański",
                  Id = "CRC",

                },
                new() {
                  Name = "colon salwadorski",
                  Id = "SVC",

                },
                new() {
                  Name = "cordoba oro (Nikaragua)",
                  Id = "NIO",

                },
                new() {
                  Name = "dalasi (Gambia)",
                  Id = "GMD",

                },
                new() {
                  Name = "denar (Macedonia Północna)",
                  Id = "MKD",

                },
                new() {
                  Name = "dinar algierski",
                  Id = "DZD",

                },
                new() {
                  Name = "dinar bahrajski",
                  Id = "BHD",

                },
                new() {
                  Name = "dinar iracki",
                  Id = "IQD",

                },
                new() {
                  Name = "dinar jordański",
                  Id = "JOD",

                },
                new() {
                  Name = "dinar kuwejcki",
                  Id = "KWD",

                },
                new() {
                  Name = "dinar libijski",
                  Id = "LYD",

                },
                new() {
                  Name = "dinar serbski",
                  Id = "RSD",

                },
                new() {
                  Name = "dinar tunezyjski",
                  Id = "TND",

                },
                new() {
                  Name = "dirham marokański",
                  Id = "MAD",

                },
                new() {
                  Name = "dirham ZEA (Zjednoczone Emiraty Arabskie)",
                  Id = "AED",

                },
                new() {
                  Name = "dobra (Wyspy Świętego Tomasza i Książęca)",
                  Id = "STN",

                },
                new() {
                  Name = "dolar bahamski",
                  Id = "BSD",

                },
                new() {
                  Name = "dolar barbadoski",
                  Id = "BBD",

                },
                new() {
                  Name = "dolar belizeński",
                  Id = "BZD",

                },
                new() {
                  Name = "dolar brunejski",
                  Id = "BND",

                },
                new() {
                  Name = "dolar Fidżi",
                  Id = "FJD",

                },
                new() {
                  Name = "dolar gujański",
                  Id = "GYD",

                },
                new() {
                  Name = "dolar jamajski",
                  Id = "JMD",

                },
                new() {
                  Name = "dolar liberyjski",
                  Id = "LRD",

                },
                new() {
                  Name = "dolar namibijski",
                  Id = "NAD",

                },
                new() {
                  Name = "dolar surinamski",
                  Id = "SRD",

                },
                new() {
                  Name = "dolar Trynidadu i Tobago",
                  Id = "TTD",

                },
                new() {
                  Name = "dolar wschodniokaraibski",
                  Id = "XCD",

                },
                new() {
                  Name = "dolar Wysp Salomona",
                  Id = "SBD",

                },
                new() {
                  Name = "dong (Wietnam)",
                  Id = "VND",

                },
                new() {
                  Name = "dram (Armenia)",
                  Id = "AMD",

                },
                new() {
                  Name = "escudo Zielonego Przylądka",
                  Id = "CVE",

                },
                new() {
                  Name = "florin arubański",
                  Id = "AWG",

                },
                new() {
                  Name = "frank burundyjski",
                  Id = "BIF",

                },
                new() {
                  Name = "frank CFA BCEAO ",
                  Id = "XOF",

                },
                new() {
                  Name = "frank CFA BEAC",
                  Id = "XAF",

                },
                new() {
                  Name = "frank CFP  ",
                  Id = "XPF",

                },
                new() {
                  Name = "frank Dżibuti",
                  Id = "DJF",

                },
                new() {
                  Name = "frank gwinejski",
                  Id = "GNF",

                },
                new() {
                  Name = "frank Komorów",
                  Id = "KMF",

                },
                new() {
                  Name = "frank kongijski (Dem. Republika Konga)",
                  Id = "CDF",

                },
                new() {
                  Name = "frank rwandyjski",
                  Id = "RWF",

                },
                new() {
                  Name = "funt egipski",
                  Id = "EGP",

                },
                new() {
                  Name = "funt gibraltarski",
                  Id = "GIP",

                },
                new() {
                  Name = "funt libański",
                  Id = "LBP",

                },
                new() {
                  Name = "funt południowosudański",
                  Id = "SSP",

                },
                new() {
                  Name = "funt sudański",
                  Id = "SDG",

                },
                new() {
                  Name = "funt syryjski",
                  Id = "SYP",

                },
                new() {
                  Name = "Ghana cedi ",
                  Id = "GHS",

                },
                new() {
                  Name = "gourde (Haiti)",
                  Id = "HTG",

                },
                new() {
                  Name = "guarani (Paragwaj)",
                  Id = "PYG",

                },
                new() {
                  Name = "gulden Antyli Holenderskich",
                  Id = "ANG",

                },
                new() {
                  Name = "kina (Papua-Nowa Gwinea)",
                  Id = "PGK",

                },
                new() {
                  Name = "kip (Laos)",
                  Id = "LAK",

                },
                new() {
                  Name = "kwacha malawijska",
                  Id = "MWK",

                },
                new() {
                  Name = "kwacha zambijska",
                  Id = "ZMW",

                },
                new() {
                  Name = "kwanza (Angola)",
                  Id = "AOA",

                },
                new() {
                  Name = "kyat (Myanmar, Birma)",
                  Id = "MMK",

                },
                new() {
                  Name = "lari (Gruzja)",
                  Id = "GEL",

                },
                new() {
                  Name = "lej Mołdawii",
                  Id = "MDL",

                },
                new() {
                  Name = "lek (Albania)",
                  Id = "ALL",

                },
                new() {
                  Name = "lempira (Honduras)",
                  Id = "HNL",

                },
                new() {
                  Name = "leone (Sierra Leone)",
                  Id = "SLE",

                },
                new() {
                  Name = "lilangeni (Eswatini)",
                  Id = "SZL",

                },
                new() {
                  Name = "loti (Lesotho)",
                  Id = "LSL",

                },
                new() {
                  Name = "manat azerbejdżański",
                  Id = "AZN",

                },
                new() {
                  Name = "metical (Mozambik)",
                  Id = "MZN",

                },
                new() {
                  Name = "naira (Nigeria)",
                  Id = "NGN",

                },
                new() {
                  Name = "nakfa (Erytrea)",
                  Id = "ERN",

                },
                new() {
                  Name = "nowy dolar tajwański",
                  Id = "TWD",

                },
                new() {
                  Name = "nowy manat (Turkmenistan)",
                  Id = "TMT",

                },
                new() {
                  Name = "ouguiya (Mauretania)",
                  Id = "MRU",

                },
                new() {
                  Name = "pa'anga (Tonga)",
                  Id = "TOP",

                },
                new() {
                  Name = "pataca (Makau)",
                  Id = "MOP",

                },
                new() {
                  Name = "peso argentyńskie",
                  Id = "ARS",

                },
                new() {
                  Name = "peso dominikańskie",
                  Id = "DOP",

                },
                new() {
                  Name = "peso kolumbijskie",
                  Id = "COP",

                },
                new() {
                  Name = "peso kubańskie",
                  Id = "CUP",

                },
                new() {
                  Name = "peso urugwajskie",
                  Id = "UYU",

                },
                new() {
                  Name = "pula (Botswana)",
                  Id = "BWP",

                },
                new() {
                  Name = "quetzal (Gwatemala)",
                  Id = "GTQ",

                },
                new() {
                  Name = "rial irański",
                  Id = "IRR",

                },
                new() {
                  Name = "rial jemeński",
                  Id = "YER",

                },
                new() {
                  Name = "rial katarski",
                  Id = "QAR",

                },
                new() {
                  Name = "rial omański",
                  Id = "OMR",

                },
                new() {
                  Name = "rial saudyjski",
                  Id = "SAR",

                },
                new() {
                  Name = "riel (Kambodża)",
                  Id = "KHR",

                },
                new() {
                  Name = "rubel białoruski",
                  Id = "BYN",

                },
                new() {
                  Name = "rubel rosyjski",
                  Id = "RUB",

                },
                new() {
                  Name = "rupia lankijska",
                  Id = "LKR",

                },
                new() {
                  Name = "rupia (Malediwy)",
                  Id = "MVR",

                },
                new() {
                  Name = "rupia Mauritiusu",
                  Id = "MUR",

                },
                new() {
                  Name = "rupia nepalska",
                  Id = "NPR",

                },
                new() {
                  Name = "rupia pakistańska",
                  Id = "PKR",

                },
                new() {
                  Name = "rupia seszelska",
                  Id = "SCR",

                },
                new() {
                  Name = "sol (Peru)",
                  Id = "PEN",

                },
                new() {
                  Name = "som (Kirgistan)",
                  Id = "KGS",

                },
                new() {
                  Name = "somoni (Tadżykistan)",
                  Id = "TJS",

                },
                new() {
                  Name = "sum (Uzbekistan)",
                  Id = "UZS",

                },
                new() {
                  Name = "szyling kenijski",
                  Id = "KES",

                },
                new() {
                  Name = "szyling somalijski",
                  Id = "SOS",

                },
                new() {
                  Name = "szyling tanzański",
                  Id = "TZS",

                },
                new() {
                  Name = "szyling ugandyjski",
                  Id = "UGX",

                },
                new() {
                  Name = "taka (Bangladesz)",
                  Id = "BDT",

                },
                new() {
                  Name = "tala (Samoa)",
                  Id = "WST",

                },
                new() {
                  Name = "tenge (Kazachstan)",
                  Id = "KZT",

                },
                new() {
                  Name = "tugrik (Mongolia)",
                  Id = "MNT",

                },
                new() {
                  Name = "vatu (Vanuatu)",
                  Id = "VUV",

                },
                new() {
                  Name = "wymienialna marka (Bośnia i Hercegowina)",
                  Id = "BAM",

                },
                new() {
                  Name = "złoto Zimbabwe",
                  Id = "ZWG",
                }
            ];
        }
        private IEnumerable<Source> GetSources()
        {
            return [
                new ()
                {
                    Name = _configuration.GetValue<string>("Source:Name"),
                    Type = TableType.TableA
                },
                new ()
                {
                    Name = _configuration.GetValue<string>("Source:Name"),
                    Type = TableType.TableB
                }
                ];
        }
    }
}
