﻿using System;
using System.Collections.Generic;

namespace BurqAuthRestSharp
{
    public class AuthorizationMetaData
    {
        private static readonly IDictionary<string, Func<string>> AppMetadataMap = new Dictionary<string, Func<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Http"] = HttpAuth.GetMetaData,
            ["HttpBasic"] = HttpBasicAuth.GetMetaData,
            ["HttpToken"] = HttpTokenAuth.GetMetaData,

            // Add Apps
            ["ShopifyToken"] = ShopifyTokenAuth.GetMetaData,
            ["ShopifyBasic"] = ShopifyBasicAuth.GetMetaData,
            ["Magento"] = MagentoAuth.GetMetaData,
            ["Woocommerce"] = WooCommerceAuth.GetMetaData,
        };

        /// <summary>
        /// Retrieves the authorization metadata for a given application.
        /// </summary>
        /// <param name="appName">The name of the application to retrieve metadata for.</param>
        /// <returns>A JSON string containing the metadata.</returns>
        public static string GetMetaData(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
                throw new ArgumentException("App name cannot be null or whitespace.", nameof(appName));

            if (!AppMetadataMap.TryGetValue(appName, out Func<string> metadataFunc))
                throw new NotSupportedException($"The app name '{appName}' is not supported.");

            return metadataFunc();
        }
    }
}