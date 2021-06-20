namespace DOfficeCore.Models
{
    public class Segment
    {
        public string Sector { get; set; }
        public string Block { get; set; }
        public string Line { get; set; }
        public override string ToString()
        {
            return $"Секция: {Sector}, Раздел: {Block}, Строка: {Line}";
        }

        public static Segment CloneSegment(Segment other)
        {
            if (other is null) return null;

            return new Segment
            {
                Sector = other.Sector,
                Block = other.Block,
                Line = other.Line,
            };
        }

        public bool Equals(Segment other)
        {
            if (this is null || other is null)
                return this is null && other is null;

            if (Sector is null || other.Sector is null)
                return Sector is null && other.Sector is null;

            if (Sector == other.Sector)
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

        public override bool Equals(object other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            return Equals(other as Segment);
        }
    }
}
