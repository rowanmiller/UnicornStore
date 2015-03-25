using System;
using System.Reflection;
using System.Linq;
using Microsoft.Data.Entity;

namespace UnicornStore.AspNet.Models
{
    public interface IAddress
    {
        string Addressee { get; set; }
        string LineOne { get; set; }
        string LineTwo { get; set; }
        string CityOrTown { get; set; }
        string StateOrProvince { get; set; }
        string ZipOrPostalCode { get; set; }
        string Country { get; set; }
    }

    public static class IAddressExtensions
    {
        public static void CopyTo(this IAddress from, IAddress to)
        {
            to.Addressee = from.Addressee;
            to.LineOne = from.LineOne;
            to.LineTwo = from.LineTwo;
            to.CityOrTown = from.CityOrTown;
            to.StateOrProvince = from.StateOrProvince;
            to.ZipOrPostalCode = from.ZipOrPostalCode;
            to.Country = from.Country;
        }

        public static TAddress CloneTo<TAddress>(this IAddress from)
            where TAddress : IAddress, new()
        {
            var to = new TAddress();
            from.CopyTo(to);
            return to;
        }

        public static void ConfigureAddress<TAddress>(this ModelBuilder.EntityBuilder<TAddress> builder)
            where TAddress : class, IAddress
        {
            var propertyMethod = builder.GetType()
                        .GetMethod("Property", new Type[] { typeof(string) })
                        .MakeGenericMethod(typeof(string));

            var requiredProps = builder.Metadata.Properties
                .Where(p => p.PropertyType == typeof(string))
                .Where(p => p.Name != nameof(IAddress.LineTwo));

            foreach (var item in requiredProps)
            {
                var propertyBuilder = ((ModelBuilder.EntityBuilder.PropertyBuilder)propertyMethod
                    .Invoke(builder, new object[] { item.Name }))
                    .Required();
            }
        }
    }
}