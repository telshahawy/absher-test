﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Common
{
    public abstract class CommandBase<T> : IRequest<T>
    {
    }
}
