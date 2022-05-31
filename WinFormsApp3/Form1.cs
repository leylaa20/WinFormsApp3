using System.Text.Json;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Student> students = new();

        private void button1_Click(object sender, EventArgs e)
        {
            Student student = new Student
            {
                Name = tbox_name.Text.ToString(),
                Surname = tbox_surname.Text.ToString(),
                Email = tbox_email.Text.ToString(),
                PhoneNumber = tbox_phone.Text.ToString(),
                BirthDate = dateTimePicker1.Value
            };


            if (tbox_name.Text == string.Empty) MessageBox.Show("Name can't be empty");

            if (tbox_surname.Text == string.Empty) MessageBox.Show("Surname can't be empty");

            if (tbox_email.Text == string.Empty) MessageBox.Show("Email can't be empty");

            if (tbox_phone.Text == string.Empty) MessageBox.Show("Phone can't be empty");

            if (tbox_phone.Text.ToArray().Count() < 10)
                MessageBox.Show("Phone number can't be less than 10");

            else if (tbox_phone.Text.ToArray().Count() > 10)
                MessageBox.Show("Phone number can't be more than 10");


            if (button1.Text == "Add")
            {
                if (!listBox1.Items.Contains(tbox_name.Text))
                {
                    students.Add(student);
                    listBox1.Items.Add(student.Name);
                    tbox_name.Text = string.Empty;
                    tbox_surname.Text = string.Empty;
                    tbox_email.Text = string.Empty;
                    tbox_phone.Text = string.Empty;
                    
                }
                else MessageBox.Show("This item alredy exist in Lisbox!");


            }
            else if (button1.Text == "Change")
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
                students.Add(student);
                listBox1.Items.Add(student.Name);
                tbox_name.Text = string.Empty;
                tbox_surname.Text = string.Empty;
                tbox_email.Text = string.Empty;
                tbox_phone.Text = string.Empty;
                
            }
            button1.Text = "Add";
        }

        private void lbl_save_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
                MessageBox.Show("Enter a name to save the file");

            else
            {
                string filePath = $"{textBox1.Text}.json";
                if (File.Exists(filePath))
                    MessageBox.Show("File already exists, enter another name");


                var JsonStr = JsonSerializer.Serialize(students);

                File.WriteAllText(filePath, JsonStr);
                MessageBox.Show("Data Added Successfully");

                listBox1.Items.Clear();
                textBox1.Text = string.Empty;
            }
        }

        private void lbl_load_Click(object sender, EventArgs e)
        {
            string filePath = $"{textBox1.Text}.json";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("No credentials exist");
                return;
            }
          
            Student[] student = null;

            var stringData = File.ReadAllText($"{textBox1.Text}.json");
            student = JsonSerializer.Deserialize<Student[]>(stringData);

            foreach (var item in student)
            {
                listBox1.Items.Add(item.Name);
                students.Add(item);
            }
            MessageBox.Show("Data Successfully Loaded");
        }

        private void tbox_phone_TextChanged(object sender, EventArgs e)
        {
            if (!tbox_phone.Text.All(char.IsDigit))
                MessageBox.Show("Phone can't contain letters or symbols");
        }

        private void tbox_name_TextChanged(object sender, EventArgs e)
        {
            if (!tbox_name.Text.All(char.IsLetter) || !tbox_surname.Text.All(char.IsLetter))
                MessageBox.Show("Can't contain digit or symbols");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var student in students)
            {
                if (listBox1.SelectedItem == student.Name)
                {
                    tbox_name.Text = student.Name;
                    tbox_surname.Text = student.Surname;
                    tbox_email.Text = student.Email;
                    tbox_phone.Text = student.PhoneNumber;
                    dateTimePicker1.Value = student.BirthDate;
                }
            }
            button1.Text = "Change";
        }
    }
}