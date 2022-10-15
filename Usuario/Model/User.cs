namespace Usuario.Model
{
    public class User
    {
        public User(int id, string name, DateTime dataNascimento, string cPF, string endereco)
        {
            Id = id;
            Name = name;
            DataNascimento = dataNascimento;
            CPF = cPF;
            Endereco = endereco;
        }

        public int Id { get; }
        public string Name { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string CPF { get; }
        public string Endereco { get; }

        public void AtualizaNomeEDataNascimento(string nome, DateTime dataNascimento)
        {
            Name = nome;
            DataNascimento = dataNascimento;
        }

    }
}
