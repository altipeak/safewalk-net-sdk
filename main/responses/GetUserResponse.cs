using System;
using System.Collections.Generic;

namespace safewalk
{
    public class GetUserResponse : BaseResponse
    {
        private String dbMobilePhone;
        private String dbEmail;
        private String ldapMobilePhone;
        private String ldapEmail;
        private UserStorage? userStorage;
        private String firstName;
        private String lastName;
        private String dn;
        private String username;
        private Boolean? locked;

        public GetUserResponse(int httpCode
                            , Dictionary<String, String> attributes
                            , String username
                            , String firstName
                            , String lastName
                            , String dn
                            , String dbMobilePhone
                            , String dbEmail
                            , String ldapMobilePhone
                            , String ldapEmail
                            , UserStorage? userStorage
                            , Boolean locked) : base(httpCode, attributes)
        {
            this.dbMobilePhone = dbMobilePhone;
            this.dbEmail = dbEmail;
            this.ldapMobilePhone = ldapMobilePhone;
            this.ldapEmail = ldapEmail;
            this.userStorage = userStorage;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dn = dn;
            this.username = username; 
            this.locked = locked; 
        }

        public GetUserResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
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
            this.locked = null;
        }
    }
}
