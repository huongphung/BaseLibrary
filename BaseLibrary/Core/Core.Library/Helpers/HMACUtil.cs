using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Core.Library.Helpers
{
    public class HMACUtil
    {
        public static IConfiguration _configuration;

        public static bool ValidateSignature(string secureKey, string dataSign, string sign)
        {
            try
            {
                if (string.IsNullOrEmpty(secureKey)) return false;
                string signSystem = HmacGenerator(dataSign, secureKey + "", "SHA1");
                if (signSystem == sign)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //commonLogger.ErrorFormat("ValidateSignature: partnerCode= {0}, dataSign= {1}, sign= {2}, ex= {3}", partnerCode, dataSign, ex.Message);
                return false;
            }
        }
        public static string HmacGeneratorForMomoRequest(string cashierCode, string shopcode, string agent, string auditNumber, string type = "SHA1")
        {
            //MOMO_SECURE_KEY
            var key = System.Configuration.ConfigurationManager.AppSettings["MOMO_SECURE_KEY"];
            var input = string.Concat(cashierCode, auditNumber, shopcode, agent);
            return HmacGenerator(input, key, type);
        }
        public static string HmacGenerator(string input, string key, string type = "SHA1")
        {
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(key);
                string retVal = "";
                switch (type.ToUpper().Trim())
                {
                    case "SHA1":
                        HMACSHA1 myhmacsha1 = new HMACSHA1(bKey);
                        byte[] byteArray = Encoding.ASCII.GetBytes(input);
                        MemoryStream stream = new MemoryStream(byteArray);
                        retVal = myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
                        stream.Close();
                        break;
                    case "SHA256":
                        HMACSHA256 myhmacsha256 = new HMACSHA256(bKey);
                        byte[] byteArray256 = Encoding.ASCII.GetBytes(input);
                        MemoryStream stream256 = new MemoryStream(byteArray256);
                        retVal = myhmacsha256.ComputeHash(stream256).Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
                        stream256.Close();
                        break;

                    case "SHA384":
                        HMACSHA384 myhmacsha384 = new HMACSHA384(bKey);
                        byte[] byteArray384 = Encoding.ASCII.GetBytes(input);
                        MemoryStream stream384 = new MemoryStream(byteArray384);
                        retVal = myhmacsha384.ComputeHash(stream384).Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
                        stream384.Close();
                        break;

                    case "SHA512":
                        HMACSHA512 myhmacsha512 = new HMACSHA512(bKey);
                        byte[] byteArray512 = Encoding.ASCII.GetBytes(input);
                        MemoryStream stream512 = new MemoryStream(byteArray512);
                        retVal = myhmacsha512.ComputeHash(stream512).Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
                        stream512.Close();
                        break;

                    case "MD5":
                        HMACMD5 myhmacshamd5 = new HMACMD5(bKey);
                        byte[] byteArrayMd5 = Encoding.ASCII.GetBytes(input);
                        MemoryStream streamMd5 = new MemoryStream(byteArrayMd5);
                        retVal = myhmacshamd5.ComputeHash(streamMd5).Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
                        streamMd5.Close();
                        break;

                    default:
                        HMACSHA1 myhmacshaDefault = new HMACSHA1(bKey);
                        byte[] byteArrayDefault = Encoding.ASCII.GetBytes(input);
                        MemoryStream streamDefault = new MemoryStream(byteArrayDefault);
                        retVal = myhmacshaDefault.ComputeHash(streamDefault).Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
                        streamDefault.Close();
                        break;
                }

                return retVal;
            }
            catch (Exception ex)
            {
                //crytoLogger.ErrorFormat("HmacGenerator: input={0}, key={1}, type={2}, ex={3}", input, key, type, ex.Message);
                return "";
            }
        }
        public static string CreateAuditNumber(string cashierCode = "")
        {
            Random random = new Random();
            return DateTime.Now.ToString("yyMMddHHmmssfff") + (999 + random.Next(900));
        }
        public static string HmacGeneratorForInvoiceRequest(string invoiceSecurityKey, string cashierCode, string shopcode, string agent, string auditNumber, string type = "SHA1")
        {
            //INVOICE_SECURE_KEY
            //var key = ConfigurationManager.AppSettings["INVOICE_SECURE_KEY"];
            var input = string.Concat(cashierCode, auditNumber, shopcode, agent);
            return HmacGenerator(input, invoiceSecurityKey, type);
        }
        public static string HmacGeneratorForPartnerRequest(string securityKey, string requestID, string parterCode, string type = "SHA1")
        {
            //INVOICE_SECURE_KEY
            //var key = ConfigurationManager.AppSettings["PARTNER_SECURE_KEY"];
            var input = !string.IsNullOrEmpty(requestID) ? string.Concat(requestID, parterCode) : string.Empty;
            return HmacGenerator(input, securityKey, type);
        }
    }
}
