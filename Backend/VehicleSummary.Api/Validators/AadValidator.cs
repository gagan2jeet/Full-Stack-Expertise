﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace VehicleSummary.Api.Validators
{
    /// <summary>
    /// Generic class that validates token issuer from the provided Azure AD authority
    /// </summary>
    public static class AadValidator
    {
        /// <summary>
        /// Validate the issuer for multi-tenant applications of various audience (Work and School account, or Work and School accounts +
        /// Personal accounts)
        /// </summary>
        /// <param name="issuer">Issuer to validate (will be tenanted)</param>
        /// <param name="securityToken">Received Security Token</param>
        /// <param name="validationParameters">Token Validation parameters</param>
        /// <remarks>The issuer is considered as valid if it has the same http scheme and authority as the
        /// authority from the configuration file, has a tenant Id, and optionally v2.0 (this web api
        /// accepts both V1 and V2 tokens).
        /// Authority aliasing is also taken into account</remarks>
        /// <returns>The <c>issuer</c> if it's valid, or otherwise <c>SecurityTokenInvalidIssuerException</c> is thrown</returns>
        public static string ValidateAadIssuer(string issuer, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            JwtSecurityToken jwtToken = securityToken as JwtSecurityToken;
            if (jwtToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken), $"{nameof(securityToken)} cannot be null.");
            }

            if (validationParameters == null)
            {
                throw new ArgumentNullException(nameof(validationParameters), $"{nameof(validationParameters)} cannot be null.");
            }

            // Extract the tenant Id from the claims
            string tenantId = jwtToken.Claims.FirstOrDefault(c => c.Type == "tid")?.Value;
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                throw new SecurityTokenInvalidIssuerException("The `tid` claim is not present in the token obtained from Azure Active Directory.");
            }

            // Build a list of valid tenanted issuers from the provided TokenValidationParameters.
            List<string> allValidTenantedIssuers = new List<string>();

            IEnumerable<string> validIssuers = validationParameters.ValidIssuers;
            if (validIssuers != null)
            {
                allValidTenantedIssuers.AddRange(validIssuers.Select(i => TenantedIssuer(i, tenantId)));
            }

            if (validationParameters.ValidIssuer != null)
            {
                allValidTenantedIssuers.Add(TenantedIssuer(validationParameters.ValidIssuer, tenantId));
            }

            // Consider the aliases (https://login.microsoftonline.com (v2.0 tokens) => https://sts.windows.net (v1.0 tokens) )
            allValidTenantedIssuers.AddRange(allValidTenantedIssuers.Select(i => i.Replace("https://sts.windows.net", "https://login.microsoftonline.com")).ToArray());

            // Consider tokens provided both by v1.0 and v2.0 issuers
            allValidTenantedIssuers.AddRange(allValidTenantedIssuers.Select(i => i.Insert(i.Length, "v2.0")).ToArray());

            if (!allValidTenantedIssuers.Contains(issuer))
            {
                //todo: add the logic for validating the details
                throw new SecurityTokenInvalidIssuerException("Issuer does not match any of the valid issuers provided for this application.");
            }
            else
            {
                return issuer;
            }
        }

        /// <summary>
        /// Validate the security token for multi-tenant applications of various audience (Work and School account, or Work and School accounts +
        /// Personal accounts)
        /// </summary>
        /// <param name="token">Received Security Token</param>
        /// <param name="validationParameters">Token Validation parameters</param>
        /// <remarks>The security token can be updated based on the defined rules</remarks>
        /// <returns>The <c>token</c> if it's valid, or otherwise <c>SecurityTokenInvalidException</c> is thrown</returns>
        public static SecurityToken ValidateSecurityToken(string token, TokenValidationParameters validationParameters)
        {
            var jwtToken = new JwtSecurityToken(token);
            if (jwtToken == null)
            {
                throw new ArgumentNullException(nameof(token), $"{nameof(token)} cannot be null.");
            }

            if (validationParameters == null)
            {
                throw new ArgumentNullException(nameof(validationParameters), $"{nameof(validationParameters)} cannot be null.");
            }
            return jwtToken;
        }

        private static string TenantedIssuer(string i, string tenantId)
        {
            return i.Replace("{tenantid}", tenantId);
        }
    }
}
