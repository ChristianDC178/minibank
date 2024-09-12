using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Aggregation;
using NRedisStack.Search.Literals.Enums;
using StackExchange.Redis;

namespace MiniBank.AccountsSrv.Infrastructure.Cache
{
    internal class Cache
    {

        /*

       IBloomCommands bf = db.BF();
       ICuckooCommands cf = db.CF();
       ICmsCommands cms = db.CMS();
       IGraphCommands graph = db.GRAPH();
       ITopKCommands topk = db.TOPK();
       ITdigestCommands tdigest = db.TDIGEST();
       ISearchCommands ft = db.FT();
       IJsonCommands json = db.JSON();
       ITimeSeriesCommands ts = db.TS();
    */


        public static void TestRedisCache()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();

            db.StringSet("foo", "bar");

            var hash = new HashEntry[] {
        new HashEntry("name", "John"),
        new HashEntry("surname", "Smith"),
        new HashEntry("company", "Redis"),
        new HashEntry("age", "29"),
        };
            db.HashSet("user-session:123", hash);

            var hashFields = db.HashGetAll("user-session:123");


            //Save json 

            var ft = db.FT();
            var json = db.JSON();

            var user1 = new
            {
                name = "Paul John",
                email = "paul.john@example.com",
                age = 42,
                city = "London"
            };

            var user2 = new
            {
                name = "Eden Zamir",
                email = "eden.zamir@example.com",
                age = 29,
                city = "Tel Aviv"
            };

            var user3 = new
            {
                name = "Paul Zamir",
                email = "paul.zamir@example.com",
                age = 35,
                city = "Tel Aviv"
            };


            var schema = new Schema()
            .AddTextField(new FieldName("$.name", "name"))
            .AddTagField(new FieldName("$.city", "city"))
            .AddNumericField(new FieldName("$.age", "age"));

            ft.Create(
                "idx:users",
                new FTCreateParams().On(IndexDataType.JSON).Prefix("user:"),
                schema);


            json.Set("user:1", "$", user1);
            json.Set("user:2", "$", user2);
            json.Set("user:3", "$", user3);

            var res = ft.Search("idx:users", new Query("Paul @age:[30 40]")).Documents.Select(x => x["json"]);
            Console.WriteLine(string.Join("\n", res));

            var res_cities = ft.Search("idx:users", new Query("Paul").ReturnFields(new FieldName("$.city", "city"))).Documents.Select(x => x["city"]);
            Console.WriteLine(string.Join(", ", res_cities));
            // Prints: London, Tel Aviv


            var request = new AggregationRequest("*").GroupBy("@city", Reducers.Count().As("count"));
            var result = ft.Aggregate("idx:users", request);

            for (var i = 0; i < result.TotalResults; i++)
            {
                var row = result.GetRow(i);
                Console.WriteLine($"{row["city"]} - {row["count"]}");
            }
        }


    }
}
