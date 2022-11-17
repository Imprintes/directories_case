using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tApp;
using tBase;
using tMvvm;

namespace directories_case
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Dict.Add("Conf", Conf);
            tCore<string, DirectoryConf>.CreateConf(Json_EventPath, Dict);
            tCore<string,DirectoryConf>.LoadConf(Json_EventPath, Dict);
            this.DataContext = Conf;
        }
        string Json_EventPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Assembly.GetEntryAssembly().GetName().Name + ".conf");
        private Dictionary<string, DirectoryConf> Dict = new Dictionary<string, DirectoryConf>();
        private DirectoryConf conf = new DirectoryConf();
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotityPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        [IgnoreDataMember]
        public DirectoryConf Conf { get { return conf; } set { conf = value; NotityPropertyChanged("Conf"); } }
    }
    public class DirectoryConf :IBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotityPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private ObservableCollection<string> srcComDirectorys { get; set; } = new ObservableCollection<string>() { "1", "2" };
        public ObservableCollection<string> SrcComDirectorys { get { return srcComDirectorys; } set { srcComDirectorys = value;NotityPropertyChanged("SrcComDirectorys"); } }
        public int srcComSelectedIndex = 0;
        [IgnoreDataMember]
        public int SrcComSelectedIndex { get { return srcComSelectedIndex; } set { srcComSelectedIndex = value;/*Add refresh method*/ NotityPropertyChanged("SrcComSelectedIndex"); } }
        private ObservableCollection<string> srcListFiles = new ObservableCollection<string>();
        public ObservableCollection<string> SrcListFiles { get { return srcListFiles; } set { srcListFiles = value; NotityPropertyChanged("SrcListFiles"); } }

        private CommandBase addSrcDir;
        public CommandBase AddSrcDir
        {
            get
            {
                if (addSrcDir == null)
                {
                    addSrcDir = new CommandBase(new Action<object>(o =>
                    {
                        string[] folder = tCore.OpenFolderDirectory();
                        foreach (string path in folder)
                            SrcComDirectorys.Add(path);
                    }));
                }
                return addSrcDir;
            }
        }
        public DirectoryConf()
        {



        }
    }

}

