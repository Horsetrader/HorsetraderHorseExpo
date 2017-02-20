using System;

namespace DAL
{
    public class BaseDAL
    {
        #region Conversion helper methods
        protected static bool toBool(string rawBool)
        {
            bool result = false;

            try
            {
                result = Convert.ToBoolean(rawBool);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        protected static bool toBool(object rawValue)
        {
            bool result = false;

            if (rawValue != null)
            {
                if (bool.TryParse(rawValue.ToString(), out result))
                {
                    return result;
                }
            }

            return result;
        }

        protected static int toInt(object rawInt)
        {
            if (rawInt != null && rawInt != DBNull.Value)
            {
                return toInt(rawInt.ToString());
            }
            else
            {
                return -1;
            }
        }

        protected static int? toIntNullable(object rawInt)
        {
            if (rawInt != null && rawInt != DBNull.Value)
            {
                return toInt(rawInt.ToString());
            }
            else
            {
                return null;
            }
        }

        protected static int toInt(string rawInt)
        {
            int result;
            if (int.TryParse(rawInt, out result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        protected static decimal? toDecimalNullable(object rawValue)
        {
            if (rawValue != null)
            {
                decimal result = 0;
                if (decimal.TryParse(rawValue.ToString(), out result))
                {
                    return result;
                }
            }
            return null;
        }
        #endregion
    }
}
