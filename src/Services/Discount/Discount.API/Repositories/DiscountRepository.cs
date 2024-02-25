using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = CreateDBConnection();
            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName,@Description,@Amount);",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (affected==0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = CreateDBConnection();
            var affected = await connection.ExecuteAsync
                ("DELETE FROM coupon WHERE ProductionName=@production",
                new { ProductName = productName });
            if (affected==0) { return false; }
            return true;
        }

        private NpgsqlConnection CreateDBConnection()
        {
            return new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
        public async Task<Coupon?> GetDiscount(string productionName)
        {
            using var connection = CreateDBConnection();
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName=@ProductName", new { ProductName = productionName });

            if (coupon==null)
            {
                return null;
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = CreateDBConnection();
            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });
            if (affected==0) { return false;}
            return true;
        }
    }
}
