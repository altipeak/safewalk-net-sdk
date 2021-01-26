using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public enum UserStorage
    {
        LDAP,
        DB,
    }
    public class CreateUserResponse
    {
        #region "vars"
        private readonly String dbMobilePhone;
        private readonly String dbEmail;
        private readonly String ldapMobilePhone;
        private readonly String ldapEmail;
        private readonly UserStorage userStorage;
        private readonly String firstName;
        private readonly String lastName;
        private readonly String dn;
        private readonly String username;
    
        private readonly int httpCode;

        private Dictionary<String, List<String>> errors;

        private const String SEPARATOR = " | ";
        #endregion

        #region "constr"
        public CreateUserResponse(int httpCode
                            , String username
                            , String firstName
                            , String lastName
                            , String dn
                            , String dbMobilePhone
                            , String dbEmail
                            , String ldapMobilePhone
                            , String ldapEmail
                            , UserStorage? userStorage)
        {
            this.dbMobilePhone = dbMobilePhone;
            this.dbEmail = dbEmail;
            this.ldapMobilePhone = ldapMobilePhone;
            this.ldapEmail = ldapEmail;
            
            if (userStorage != null)
                this.userStorage = userStorage.Value;
            
            this.firstName = firstName;
            this.lastName = lastName;
            this.dn = dn;
            this.username = username;
            this.httpCode = httpCode;
            this.errors = new Dictionary<string, List<string>>();
        }

        public CreateUserResponse(int httpCode
            , Dictionary<String, List<String>> errors)
        {
            this.dbMobilePhone = null;
            this.dbEmail = null;
            this.ldapMobilePhone = null;
            this.ldapEmail = null;
            this.userStorage = null;
            this.firstName = null;
            this.lastName = null;
            this.dn = null;
            this.username = null;
            this.httpCode = httpCode;
            this.errors = errors;
        }
        #endregion

        #region "Publics"
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.code != null)
                sb.Append(this.httpCode.ToString()).Append(SEPARATOR);
          
            if (this.httpCode == 201)
            {
                if (this.username != null)
                    sb.Append(this.username).Append(SEPARATOR);
                
                if (this.firstName != null)
                    sb.Append(this.firstName).Append(SEPARATOR);

                if (this.lastName != null)
                    sb.Append(this.lastName).Append(SEPARATOR);

                if (this.userStorage != null) 
                    sb.Append(this.userStorage).Append(SEPARATOR);

                if (this.dn != null) 
                    sb.Append(this.dn).Append(SEPARATOR);

                if (this.dbEmail != null)
                    sb.Append(this.dbEmail).Append(SEPARATOR);

                if (this.dbMobilePhone != null)
                    sb.Append(this.dbMobilePhone).Append(SEPARATOR);

                if (this.ldapEmail != null)
                    sb.Append(this.ldapEmail).Append(SEPARATOR);

                if (this.ldapMobilePhone != null)
                    sb.Append(this.ldapMobilePhone).Append(SEPARATOR);
            }
            else
            {
                foreach (KeyValuePair<String, List<String>> error in this.errors)
                {
                    sb.Append(error.Key).Append(" [");
                    foreach (String e in error.Value)
                    {
                        sb.Append(e).Append(", ");
                    }
                    sb.Append("]").Append(SEPARATOR);
                }
            }
            return sb.toString();
        }

        public Dictionary<String, List<String>> getErrors()
        {
            return errors;
        }

        /**
        * @return the dbMobilePhone
        */
        public String getDbMobilePhone()
        {
            return dbMobilePhone;
        }

        /**
         * @return the dbEmail
         */
        public String getDbEmail()
        {
            return dbEmail;
        }

        /**
         * @return the ldapMobilePhone
         */
        public String getLdapMobilePhone()
        {
            return ldapMobilePhone;
        }

        /**
         * @return the ldapEmail
         */
        public String getLdapEmail()
        {
            return ldapEmail;
        }

        /**
         * @return the userStorage
         */
        public UserStorage getUserStorage()
        {
            return userStorage;
        }

        /**
         * @return the firstName
         */
        public String getFirstName()
        {
            return firstName;
        }

        /**
         * @return the lastName
         */
        public String getLastName()
        {
            return lastName;
        }

        /**
         * @return the dn
         */
        public String getDn()
        {
            return dn;
        }

        /**
         * @return the username
         */
        public String getUsername()
        {
            return username;
        }
        #endregion
    }
}
