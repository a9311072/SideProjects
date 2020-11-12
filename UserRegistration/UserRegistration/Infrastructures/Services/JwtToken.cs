using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using UserRegistration.Infrastructures.Interfaces;

namespace UserRegistration.Infrastructures.Services
{
    public class JwtToken: IToken
    {
        private const string Secret = "AmazingTalker+ZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        private static DateTime? _now;

        internal static DateTime? Now
        {
            get
            {
                if (_now.HasValue == false)
                {
                    return DateTime.UtcNow;
                }

                return _now;
            }
            set { _now = value; }
        }

        public string GetToken(string userName, int expireMinutes)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var now = (DateTimeOffset)Now.Value;
            var expired = now.AddMinutes(expireMinutes).ToUnixTimeSeconds();
            var notBefore = now.ToUnixTimeSeconds();

            var payload = new Dictionary<string, object>
            {
                {"name", userName},
                {"exp", expired},
                {"nbf", notBefore}
            };

            var token = encoder.Encode(payload, symmetricKey);

            return token;
        }

        public string TryValidateToken(string token)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var payload = decoder.Decode(token, symmetricKey, true);
                return payload;
            }
            catch (TokenExpiredException)
            {
                return "Token has expired";
            }
            catch (SignatureVerificationException)
            {
                return "Token has invalid signature";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}