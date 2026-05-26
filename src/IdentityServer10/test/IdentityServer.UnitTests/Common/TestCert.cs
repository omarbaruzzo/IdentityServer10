/*
 Copyright (c) 2024 OmarBaruzzo, Omar Baruzzo - https://github.com/omarbaruzzo/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.UnitTests.Common
{
    internal static class TestCert
    {
        private static readonly Lazy<X509Certificate2> _cert = new(CreateOrLoad);

        public static X509Certificate2 Load() => _cert.Value;

        public static SigningCredentials LoadSigningCredentials()
        {
            return new SigningCredentials(new X509SecurityKey(Load()), "RS256");
        }

        private static X509Certificate2 CreateOrLoad()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "identityserver_testing.pfx");
            if (File.Exists(path))
            {
                return new X509Certificate2(path, "password", X509KeyStorageFlags.Exportable);
            }

            using var rsa = RSA.Create(2048);
            var request = new CertificateRequest(
                "CN=identityserver_testing",
                rsa,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);
            var generated = request.CreateSelfSigned(
                DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddYears(5));
            return new X509Certificate2(
                generated.Export(X509ContentType.Pfx, "password"),
                "password",
                X509KeyStorageFlags.Exportable);
        }
    }
}
