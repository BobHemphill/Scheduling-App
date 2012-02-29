﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
    public class ResidentYearsCount {
        public int Interns { get; set; }
        public int Senior1s { get; set; }
        public int Senior2s { get; set; }
        public int Chiefs { get; set; }
        public int Anys { get; set; }

        public ResidentYearsCount() : this(1, 1, 1, 1, 1) { }

        public ResidentYearsCount(int i, int s1, int s2, int c, int a) {
            Interns = i;
            Senior1s = s1;
            Senior2s = s2;
            Chiefs = c;
            Anys = a;
        }

        public RotationResidents this[ResidentYears index] {
            get {
                switch (index) {
                    case ResidentYears.Intern:
                        return new RotationResidents(Interns);
                    case ResidentYears.Senior1:
                        return new RotationResidents(Senior1s);
                    case ResidentYears.Senior2:
                        return new RotationResidents(Senior2s);
                    case ResidentYears.Chief:
                        return new RotationResidents(Chiefs);
                    case ResidentYears.Any:
                        return new RotationResidents(Anys);
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
