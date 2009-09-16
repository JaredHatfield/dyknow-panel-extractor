﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    class Reason
    {
        int id;
        Boolean credit;
        String description;

        public int Id
        {
            get { return id; }
        }
        public Boolean Credit
        {
            get { return credit; }
        }
        public String Description
        {
            get { return description; }
        }

        public Reason(int id, Boolean credit, String description)
        {
            this.id = id;
            this.credit = credit;
            this.description = description;
        }

        public Reason()
        {
            id = -1;
            credit = false;
            description = "";
        }
    }
}
