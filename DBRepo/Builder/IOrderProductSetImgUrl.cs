﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo.Builder
{
    internal interface IOrderProductSetImgUrl
    {
        IOrderProductSetDescription SetImgUrl(string imgUrl);
    }
}