using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScriptLoader.Entities
{
#warning Enum에 접근하기 위해 xaml에서 Entities 네임스페이스 참조하는 코드 개선 필요
    public enum Editor
    {
        Notepad = 1,
        VSCode = 2,
        Etc = 4
    }

    [Table("Setting")]
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        public string ScriptDirectory { get; set; }

        public Editor Editor { get; set; } = Editor.Notepad;

        public string EditorCommand { get; set; }
    }
}
