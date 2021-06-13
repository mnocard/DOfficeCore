using System;

namespace DOfficeCore.Models
{
    public class Section : IEquatable<Section>
    {
        // Diagnosis : string - Диагноз, в котором находится раздел
        public string Diagnosis { get; set; }

        // Block : string - Раздел, в котором находится строка
        public string Block { get; set; }

        // Line : string - Собственно сама строка
        public string Line { get; set; }

        public bool Equals(Section other)
        {
            if (this is null || other is null)
                return this is null && other is null;

            if (Diagnosis is null || other.Diagnosis is null)
                return Diagnosis is null && other.Diagnosis is null;

            if (Diagnosis == other.Diagnosis)
            {
                if (Block is null || other.Block is null)
                    return Block is null && other.Block is null;

                if (Block == other.Block)
                {
                    if (Line is null || other.Line is null)
                        return Line is null && other.Line is null;

                    return Line == other.Line;
                }
            }

            return false;
        }

        public override bool Equals(Object other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            return Equals(other as Section);
        }

        public static Section CloneSection(Section other)
        {
            if (other is null) return null;

            return new Section
            {
                Diagnosis = other.Diagnosis,
                Block = other.Block,
                Line = other.Line,
            };
        }

        public override string ToString()
        {
            return $"Секция: {Diagnosis}, Раздел: {Block}, Строка: {Line}";
        }

        // GetHashCode не переопределяем, так как данный класс может изменять свои свойства.
        //public override int GetHashCode() => HashCode.Combine(Diagnosis, Block, Line);
    }
}
