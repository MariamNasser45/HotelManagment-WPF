﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using FinalProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public partial interface IFRONTEND_RESERVATIONContextProcedures
    {
        Task<int> Add_Data_UpdateAsync(string fname, string lname, string birthday, string gender, string phonenumber, string email, int? numberguest, string streetaddress, string aptsuite, string city, string state, string zipcode, string roomtype, string roomfloor, string roomnumber, double? totalbill, string paymenttype, string cardtype, string cardnumber, string cardexp, string cardcvc, DateTime? arrivaltime, DateTime? leavingtime, bool? checkin, int? breakfast, int? lunch, int? dinner, bool? cleaning, bool? towel, bool? ssurprise, bool? supplystatus, int? foodbill, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}