using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Diagnostics;
using DevExpress.XtraBars.Navigation;
using DevExpress.Utils;
using DevExpress.XtraBars.Helpers;

namespace Math_V1._1
{
    public partial class Trangchu : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region"hethong
        public Trangchu()
        {
            InitializeComponent();
        }
        int a = 0;
        private void Trangchu_Load(object sender, EventArgs e)
        {
            Loading fr1 = new Loading();
            fr1.ShowDialog();
           
            lblTime.Caption = "";
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
            timer2.Enabled = true;
            timer2.Interval = 10000;
            timer2.Start();
            timer3.Enabled = true;
            timer3.Interval = 15000;
            timer3.Start();
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            SkinHelper.InitSkinPopupMenu(SkinsLink);
            txtten.Text = "VŨ HỮU (1437–1530) ";
            txtkienthuc.Text = "Là một nhà toán học người Việt, và cũng là một danh thần dưới triều đại Lê Thánh Tông, Lê Hiến Tông. Ông còn được coi là nhà toán học đầu tiên của Việt Nam. Ông người làng Mộ Trạch, tổng Thì Cử, huyện Đường An, phủ Thượng Hồng, trấn Hải Dương, nay là làng Mộ Trạch, xã Tân Hồng, huyện Bình Giang, tỉnh Hải Dương. Năm Quang Thuận thứ 4 đời vua Lê Thánh Tông (Quý Mùi 1463), ông đỗ Hoàng giáp.Vũ Hữu đã kinh qua các chức vụ như Khâm hình viện lang trung, Thượng thư bộ Hộ, Thượng thư bộ Lễ trong triều đình nhà Hậu Lê, sau được tặng phong Thái bảo. Mặc dù về hưu năm 70 tuổi, đển năm 90 tuổi (1527), ông vẫn được vua tin dùng, sai mang cờ tiết đi phong vương cho Mạc Đăng Dung. Khi đó ông có tước là Tùng Dương hầu.";
            txtcongtrinh.Text = "Công trình toán học ông để lại cho hậu thế nổi bật là Lập Thành Toán Pháp. Quyển sách này miêu tả các phép đo đạc cũng như cách tính xây dựng nhà cửa, thành lũy. Các phép đo ruộng đất được tính theo đơn vị mẫu, sào, thước (24 mét vuông) và tấc (1/10 thước)";
            tho.Text = "Nơi anh đến là không gian vô tận";
            tho.Text += Environment.NewLine + "Từ một điểm vẽ ra rất nhiều đường";
            tho.Text += Environment.NewLine + "Nhiều đường thẳng song song chẳng cùng chiều";
            tho.Text += Environment.NewLine + "Nhiều mặt phẳng gặp nhau nơi vô định";
            tho.Text += Environment.NewLine + "";
            tho.Text += Environment.NewLine + "Nơi quĩ tích là những đường đã định";
            tho.Text += Environment.NewLine + "Như thái dương có tâm điểm mặt trời";
            tho.Text += Environment.NewLine + "Khi trái đất luôn xoay quanh rã rời";
            tho.Text += Environment.NewLine + "Vẫn phải giữ đường xoay trong quỹ đạo";
            tho.Text += Environment.NewLine + "";
            tho.Text += Environment.NewLine + "Đường anh đi bất biến do đào tạo";
            tho.Text += Environment.NewLine + "Rất thẳng ngay nên không có đạo hàm";
            tho.Text += Environment.NewLine + "Vì đường thẳng nên dễ tính nguyên hàm ";
            tho.Text += Environment.NewLine + "Còn ẩn số thì không cần phải kiếm";
            tho.Text += Environment.NewLine + "";
            tho.Text += Environment.NewLine + "Tình yêu của anh giống như hằng số";
            tho.Text += Environment.NewLine + "Chẳng mập mờ như căn số phải tìm";
            tho.Text += Environment.NewLine + "Nghiệm thực tình vui, nghiệm ảo nát tim";
            tho.Text += Environment.NewLine + "Là hằng số tình anh không biến đổi";
            tho.Text += Environment.NewLine + "";
            tho.Text += Environment.NewLine + "Bởi em thích tình yêu trong dời đổi";
            tho.Text += Environment.NewLine + "Phép vi phân em chẻ nhỏ tình yêu";
            tho.Text += Environment.NewLine + "Dùng vị tự em đổi tình ngược chiều";
            tho.Text += Environment.NewLine + "Hằng số tiêu và tình yêu vô nghiệm";
            tho.Text += Environment.NewLine + "";
            tho.Text += Environment.NewLine + "Em yêu ơi! Tình đâu cần phải kiếm";
            tho.Text += Environment.NewLine + "Ngay trong em đã có một trái tim";
            tho.Text += Environment.NewLine + "Tình trong tim, sao cứ mãi kiếm tìm";
            tho.Text += Environment.NewLine + "Bằng toán học, sao tìm ra đáp số ???";
        }
     
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Caption = (DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + " " + DateTime.Now.DayOfWeek.ToString() + ", " + (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) + "/" + (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year;
         
        }
        #endregion
         
        #region"moform
        private void bt_ElementClick(object sender, NavElementEventArgs e)
        {
            Duongthang fr = new Duongthang();
            fr.ShowDialog();
        }
        private void navButton3_ElementClick(object sender, NavElementEventArgs e)
        {
            Duongtron fr = new Duongtron();
            fr.ShowDialog();
        }
        private void navButton4_ElementClick(object sender, NavElementEventArgs e)
        {
              DuongElip fr = new DuongElip();
            fr.ShowDialog();
        }

        private void navButton5_ElementClick(object sender, NavElementEventArgs e)
        {
            Phepbienhinh fr = new Phepbienhinh();
            fr.ShowDialog();
        }
        private void tileItem1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Thuoctinhhinhhoc fr = new Thuoctinhhinhhoc();
            fr.ShowDialog();
          
        }
        private void tileItem2_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            DuongElip fr = new DuongElip();
            fr.ShowDialog();
        }

        private void tileItem3_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Thuoctinhhinhhoc fr = new Thuoctinhhinhhoc();
            fr.ShowDialog();
        }

        private void tileItem4_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Thuoctinhhinhhoc fr = new Thuoctinhhinhhoc();
            fr.ShowDialog();
        }
        #endregion
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var gallery = new DevExpress.XtraBars.Ribbon.GalleryDropDown();
            gallery.Manager = barManager1;
            SkinHelper.InitSkinGalleryDropDown(gallery);
            gallery.ShowPopup(MousePosition);
        }
      
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Tacgia fr = new Tacgia();
            fr.ShowDialog();
        }
      
        private void gmail_Click(object sender, EventArgs e)
        {
           Gmail fr = new Gmail();
            fr.ShowDialog();
        }

        private void facebook_Click(object sender, EventArgs e)
        {
            Tacgia fr = new Tacgia();
            fr.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int Numrd;
            Random rd = new Random();
            Numrd = rd.Next(1, 10);//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 10  
            if (Numrd == 1)
            {
                txtten.Text = "VŨ HỮU (1437–1530) ";
                txtkienthuc.Text = "Là một nhà toán học người Việt, và cũng là một danh thần dưới triều đại Lê Thánh Tông, Lê Hiến Tông. Ông còn được coi là nhà toán học đầu tiên của Việt Nam. Ông người làng Mộ Trạch, tổng Thì Cử, huyện Đường An, phủ Thượng Hồng, trấn Hải Dương, nay là làng Mộ Trạch, xã Tân Hồng, huyện Bình Giang, tỉnh Hải Dương. Năm Quang Thuận thứ 4 đời vua Lê Thánh Tông (Quý Mùi 1463), ông đỗ Hoàng giáp.Vũ Hữu đã kinh qua các chức vụ như Khâm hình viện lang trung, Thượng thư bộ Hộ, Thượng thư bộ Lễ trong triều đình nhà Hậu Lê, sau được tặng phong Thái bảo. Mặc dù về hưu năm 70 tuổi, đển năm 90 tuổi (1527), ông vẫn được vua tin dùng, sai mang cờ tiết đi phong vương cho Mạc Đăng Dung. Khi đó ông có tước là Tùng Dương hầu.";
                txtcongtrinh.Text = "Công trình toán học ông để lại cho hậu thế nổi bật là Lập Thành Toán Pháp. Quyển sách này miêu tả các phép đo đạc cũng như cách tính xây dựng nhà cửa, thành lũy. Các phép đo ruộng đất được tính theo đơn vị mẫu, sào, thước (24 mét vuông) và tấc (1/10 thước)";
                hinhanh.Image = Math_V1._1.Properties.Resources.vuhuu ;
            }
            if (Numrd == 2)
            {
                txtten.Text = "LÊ VĂN THIÊM (1918-1991)";
                txtkienthuc.Text = "Là Giáo sư, tiến sĩ khoa học toán học đầu tiên của Việt Nam, một trong số các nhà khoa học tiêu biểu nhất của Việt Nam trong thế kỷ 20. Lê Văn Thiêm và Hoàng Tụy là hai nhà toán học Việt Nam được chính phủ Việt Nam phong tặng Giải thưởng Hồ Chí Minh đợt 1 vào năm 1996 về những công trình toán học đặc biệt xuất sắc.";
                txtcongtrinh.Text = "Lê Văn Thiêm cùng các học trò tham gia giải quyết thành công một số vấn đề thực tiễn ở Việt Nam như:";
                txtcongtrinh.Text += Environment.NewLine + "- Tính toán nổ mìn buồng mỏ đá Núi Voi lấy đá phục vụ xây dựng khu gang thép Thái Nguyên (1964)";
                txtcongtrinh.Text += Environment.NewLine + "- Phối hợp với Cục Kỹ thuật Bộ Quốc phòng lập bảng tính toán nổ mìn làm đường (1966)";
                txtcongtrinh.Text += Environment.NewLine + "- Phối hợp với Viện Thiết kế Bộ Giao thông Vận tải tính toán nổ mìn định hướng để tiến hành nạo vét kênh Nhà Lê từ Thanh Hoá đến Hà Tĩnh (1966 – 1967)";
                txtcongtrinh.Text += Environment.NewLine + "- Tính toán nước thấm và chế độ dòng chảy cho các đập thuỷ điện Hòa Bình, Vĩnh Sơn";
                txtcongtrinh.Text += Environment.NewLine + "- Tính toán chất lượng nước cho công trình thuỷ điện Trị An";
                txtcongtrinh.Text += Environment.NewLine + " Ông là tác giả của khoảng 20 công trình toán học được đăng trên các tạp chí quốc tế";
                hinhanh.Image = Math_V1._1.Properties.Resources.levanthiem;
            }
            if (Numrd == 3)
            {
                txtten.Text = "LƯƠNG THẾ VINH (1441–1496)";
                txtkienthuc.Text = "Trạng Lường, tên tự là Cảnh Nghị, tên hiệu là Thụy Hiên, là một nhà toán học, Phật học, nhà thơ Việt Nam thời Lê sơ. Ông đỗ trạng nguyên dưới triều Lê Thánh Tông và làm quan tại viện Hàn Lâm. Ông là một trong 28 nhà thơ của hội Tao Đàn do vua Lê Thánh Tông lập năm 1495.Lương Thế Vinh sinh ngày 1 tháng Tám năm Tân Dậu (tức 17 tháng 8 năm 1441) [1] tại làng Cao Hương, huyện Thiên Bản, trấn Sơn Nam (nay là thôn Cao Phương, xã Liên Bảo, huyện Vụ Bản, tỉnh Nam Định). Từ nhỏ Lương Thế Vinh đã nổi tiếng về khả năng học mau thuộc, nhanh hiểu, và khả năng sáng tạo trong các trò chơi như đá bóng, thả diều, câu cá, bẫy chim. Năm 1463, Lương Thế Vinh đỗ Đệ nhất giáp tiến sĩ cập đệ nhất danh (trạng nguyên) khoa Quý Mùi niên hiệu Quang Thuận thứ 4, đời Lê Thánh Tông.";
                txtcongtrinh.Text = "Về toán học, Lương Thế Vinh đã để lại :";
                txtcongtrinh.Text += Environment.NewLine + "- Đại thành Toán pháp";
                txtcongtrinh.Text += Environment.NewLine + "- Khải minh Toán học";
                hinhanh.Image = Math_V1._1.Properties.Resources.luongthevinh;
            }
            if (Numrd == 4)
            {
                txtten.Text = "ISAAC NEWTON JR. (1643 - 1727)";
                txtkienthuc.Text = "Là một nhà vật lý, nhà thiên văn học, nhà triết học, nhà toán học, nhà thần học và nhà giả kim người Anh, được nhiều người cho rằng là nhà khoa học vĩ đại và có tầm ảnh hưởng lớn nhất. Theo lịch Julius, ông sinh ngày 25 tháng 12 năm 1642 và mất ngày 20 tháng 3 năm 1727; theo lịch Gregory, ông sinh ngày 4 tháng 1 năm 1643 và mất ngày 31 tháng 3 năm 1727. Luận thuyết của ông về Philosophiae Naturalis Principia Mathematica (Các Nguyên lý Toán học của Triết lý về Tự nhiên) xuất bản năm 1687, đã mô tả về vạn vật hấp dẫn và 3 định luật Newton, được coi là nền tảng của cơ học cổ điển, đã thống trị các quan niệm về vật lý, khoa học trong suốt 3 thế kỷ tiếp theo. ông cho rằng sự chuyển động của các vật thể trên mặt đất và các vật thể trong bầu trời bị chi phối bởi các định luật tự nhiên giống nhau; bằng cách chỉ ra sự thống nhất giữa Định luật Kepler về sự chuyển động của hành tinh và lí thuyết của ông về trọng lực, ông đã loại bỏ hoàn toàn Thuyết nhật tâm và theo đuổi cách mạng khoa học. Trong cơ học, Newton đưa ra nguyên lý bảo toàn động lượng (bảo toàn quán tính). Trong quang học, ông khám phá ra sự tán sắc ánh sáng, giải thích việc ánh sáng trắng qua lăng kính trở thành nhiều màu. Trong toán học, Newton cùng với Gottfried Leibniz phát triển phép tính vi phân và tích phân. Ông cũng đưa ra nhị thức Newton tổng quát.";
                txtcongtrinh.Text = "Các công trình đồ sộ về vật lí và toán học như :lực, trọng lực, ánh sáng, ... và các phép vi phân, tích phân, phương pháp khai triển nhị thưc, phương pháp lặp nghiệm, ...";
                hinhanh.Image = Math_V1._1.Properties.Resources.newton;
            }
            if (Numrd == 5)
            {
                txtten.Text = "PYTHAGORAS (580 đến 572 TCN - 500 đến 490 TCN)";
                txtkienthuc.Text = "Là một nhà triết học người Hy Lạp và là người sáng lập ra phong trào tín ngưỡng có tên học thuyết Pythagoras. Ông thường được biết đến như một nhà khoa học và toán học vĩ đại. Trong tiếng Việt, tên của ông thường được phiên âm từ tiếng Pháp (Pythagore) thành Pi-ta-go.Pythagoras đã thành công trong việc tin rằng tổng 3 góc của một tam giác bằng 180° và nổi tiếng nhất nhờ định lý toán học mang tên ông. Ông cũng được biết đến là cha đẻ của số. Ông đã có nhiều đóng góp quan trọng cho triết học và tín ngưỡng vào cuối thế kỷ 7 TCN. Về cuộc đời và sự nghiệp của ông, có quá nhiều các huyền thoại khiến việc tìm lại sự thật lịch sử không dễ. Pythagoras và các học trò của ông tin rằng mọi sự vật đều liên hệ đến toán học, và mọi sự việc đều có thể tiên đoán trước qua các chu kỳ.";
                txtcongtrinh.Text = "Không văn bản nào của Pythagoras còn tồn tại tới ngày nay, dù các tác phẩm giả mạo tên ông - hiện vẫn còn vài cuốn - đã thực sự được lưu hành vào thời xưa. Những nhà phê bình thời cổ như Aristotle và Aristoxenus đã tỏ ý nghi ngờ các tác phẩm đó. Những môn đồ Pythagoras thường trích dẫn các học thuyết của thầy với câu dẫn autos ephe (chính thầy nói) - nhấn mạnh đa số bài dạy của ông đều ở dạng truyền khẩu. Pythagoras xuất hiện với tư cách một nhân vật trong tác phẩm Metamorphoses của Ovid, trong đó Ovid đã để Pythagoras được trình bày các quan điểm của ông.";
                hinhanh.Image = Math_V1._1.Properties.Resources.pitago;
            }
            if (Numrd == 6)
            {
                txtten.Text = "ARISTOTELES (384 – 322 TCN)";
                txtkienthuc.Text = "Là một nhà triết học và bác học thời Hy Lạp cổ đại, học trò của Platon và thầy dạy của Alexandros Đại đế. Di bút của ông bao gồm nhiều lãnh vực như vật lý học, siêu hình học, thi văn, kịch nghệ, âm nhạc, luận lý học, tu từ học (rhetoric), ngôn ngữ học, Kinh tế học, chính trị học, đạo đức học, sinh học, và động vật học. Ông được xem là người đặt nền móng cho môn luận lý học. Ông cũng thiết lập một phương cách tiếp cận với triết học bắt đầu bằng quan sát và trải nghiệm trước khi đi tới tư duy trừu tượng. Cùng với Platon và Socrates, Aristoteles là một trong ba cột trụ của văn minh Hy Lạp cổ đại.";
                txtcongtrinh.Text = "Ông sỡ hữu vô vàn tác phẩm khác nhau để mọi thể loại trong mọi lĩnh vực đời sống";
                hinhanh.Image = Math_V1._1.Properties.Resources.aristot;

            }
            if (Numrd == 7)
            {
                txtten.Text = "ARCHIMEDES (287 TCN – 212 TCN)";
                txtkienthuc.Text = "Là một nhà toán học, nhà vật lý, kỹ sư, nhà phát minh, và một nhà thiên văn học người Hy Lạp. Dù ít chi tiết về cuộc đời ông được biết, ông được coi là một trong những nhà khoa học hàng đầu của thời kỳ cổ đại. Thường được xem là nhà toán học vĩ đại nhất thời cổ đại và là một trong những nhà toán học vĩ đại nhất mọi thời đại, ông đã báo trước phép vi tích phân và giải tích hiện đại bằng việc ấp dụng các khái niệm về vô cùng bé và phương pháp vét cạn để để suy ra và chứng minh chặt chẽ một loạt các định lý hình học, bao gồm các định lý về diện tích hình tròn, diện tích bề mặt và thể tích của hình cầu, cũng như diện tích dưới một đường parabol. Các thành tựu toán học khác bao gồm việc suy ra một phép xấp xỉ tương đối chính xác số pi, định nghĩa một dạng đường xoáy ốc mang tên ông, và tạo ra một hệ sử dụng phép lũy thừa để biểu thị những số lớn. Ông cũng là một trong những người đầu tiên áp dụng toán học vào các bài toán vật lý, lập nên các ngành thủy tĩnh học và tĩnh học, bao gồm lời giải thích cho nguyên lý của đòn bẩy. Ông cũng được ghi danh vì đã thiết kế ra nhiều loại máy móc, chẳng hạn bơm xoắn ốc mang tên ông, ròng rọc phức hợp, và các công cụ chiến tranh để bảo vệ quê hương ông, Syracusa. Archimedes chết trong trận bao vây Syracusa khi ông bị một tên lính La Mã giết dù đã có lệnh không được làm hại ông. Cicerocó kể lại lần tới thăm mộ Archimedes, nơi dựng một khối cầu và một ống hình trụ mà Archimedes yêu cầu đặt trên mô mình, tượng trưng cho những khám phá toán học của ông.";
                txtcongtrinh.Text = "Các tác phảm của ồng gồm có rất nhiều thể loại từ toán học, ... đến vật lý học, về toán học thì chủ yếu gồm những tác phẩm sau";
                 txtcongtrinh.Text += Environment.NewLine + "- Về việc đo đạc hình tròn";
                 txtcongtrinh.Text += Environment.NewLine + "- Về các hình xoắn ốc";
                 txtcongtrinh.Text += Environment.NewLine + "- Về hình cầu và hình trụ";
                 txtcongtrinh.Text += Environment.NewLine + "- Về các hình nêm và hình cầu";
                 txtcongtrinh.Text += Environment.NewLine + "- Người đếm cát";
                 txtcongtrinh.Text += Environment.NewLine + "...";

                hinhanh.Image = Math_V1._1.Properties.Resources.ascimet;
            }
            if (Numrd == 8)
            {
                txtten.Text = "Euclid";
                txtkienthuc.Text = "Là nhà toán học lỗi lạc thời cổ Hy Lạp, sống vào thế kỉ thứ 3 TCN. Ông được mệnh danh là cha đẻ của Hình học. Có thể nói hầu hết kiến thức hình học ở cấp trung học cơ sở hiện nay đều đã được đề cập một cách có hệ thống, chính xác trong bộ sách Cơ sở gồm 13 cuốn do Euclid viết ra, và đó cũng là bộ sách có ảnh hưởng nhất trong Lịch sử Toán học kể từ khi nó được xuất bản đến cuối thế kỷ 19 và đầu thế kỷ 20. Ngoài ra ông còn tham gia nghiên cứu về luật xa gần, đường cô-nic, lý thuyết số và tính chính xác. Tục truyền rằng có lần vua Ptolemaios I Soter hỏi Euclid rằng liệu có thể đến với hình học bằng con đường khác ngắn hơn không? Ông trả lời ngay: 'Muôn tâu Bệ hạ, trong hình học không có con đường dành riêng cho vua chúa'";
                txtcongtrinh.Text = "Bằng cách chọn lọc, phân biệt các loại kiến thức hình học đã có, bổ sung, khái quát và sắp xếp chúng lại thành một hệ thống chặt chẽ, dùng các tính chất trước để suy ra tính chất sau, bộ sách Cơ sở đồ sộ của Euclid đã đặt nền móng cho môn hình học cũng như toàn bộ toán học cổ đại. Bộ sách gồm 13 cuốn: sáu cuốn đầu gồm các kiến thức về hình học phẳng, ba cuốn tiếp theo có nội dung số học được trình bày dưới dạng hình học, cuốn thứ mười gồm các phép dựng hình có liên quan đến đại số, 3 cuốn cuối cùng nói về hình học không gian.";
                hinhanh.Image = Math_V1._1.Properties.Resources.euclid;
            }
            if (Numrd == 9)
            {
                txtten.Text = "PIERRE DE FERMAT (17/8/1601 – 12/1/1665)";
                txtkienthuc.Text = "Là một học giả nghiệp dư vĩ đại, một nhà toán học nổi tiếng và cha đẻ của lý thuyết số hiện đại. Xuất thân từ một gia đình khá giả, ông học ở Toulouse và lấy bằng cử nhân luật dân sự rồi làm chánh án. Chỉ trừ gia đình và bạn bè tâm giao, chẳng ai biết ông vô cùng say mê toán. Mãi sau khi Pierre de Fermat mất, người con trai mới in dần các công trình của cha kể từ năm 1670. Năm 1896, hầu hết các tác phẩm của Fermat được ấn hành thành 4 tập dày. Qua đó, người đời vô cùng ngạc nhiên và khâm phục trước sức đóng góp dồi dào của ông. Chính ông là người sáng lập lý thuyết số hiện đại, trong đó có 2 định lý nổi bật: định lý nhỏ Fermat và định lý lớn Fermat (định lý cuối cùng của Fermat).";
                txtcongtrinh.Text = "Định lí Fermat nhỏ và định lí Fermat lớn";
                hinhanh.Image = Math_V1._1.Properties.Resources.fermat;
            }
            if (Numrd == 10)
            {
                txtten.Text = "LENARDO DI SER PIERO DA VINCI(15/4/1452 - 5/11/1519)";
                txtkienthuc.Text = "là một họa sĩ, nhà điêu khắc, kiến trúc sư, nhạc sĩ, bác sĩ, kỹ sư, nhà giải phẫu, nhà sáng tạo và triết học tự nhiên. Ông được coi là một thiên tài toàn năng người Ý. Tên gọi của thành phố Vinci là nơi sinh của ông, nằm trong lãnh thổ của tỉnh Firenze, cách thành phố Firenze 30 km về phía Tây gần Empoli, cúng là họ của ông. Người ta gọi ông ngắn gọn là Leonardo vì da Vinci có nghĩa là đến từ Vinci, không phải là họ thật của ông. Tên khai sinh là Leonardo di ser Piero da Vinci có nghĩa là Leonardo, con của Ser Piero, đến từ Vinci. Ông là tác giả của những bức hoạ nổi tiếng như bức Mona Lisa, bức Bữa ăn tối cuối cùng. Ông là người có những ý tưởng vượt trước thời đại của mình, đặc biệt là khái niệm về máy bay trực thăng, xe tăng, dù nhảy, sự sử dụng hội tụ năng lượng mặt trời, máy tính, sơ thảo lý thuyết kiến tạo địa hình, tàu đáy kép, cùng nhiều sáng chế khác. Một vài thiết kế của ông đã được thực hiện và khả thi trong lúc ông còn sống. Ứng dụng khoa học trong chế biến kim loại và trong kỹ thuật ở thời đại Phục Hưng còn đang ở trong thời kỳ trứng nước. Thêm vào đó, ông có đóng góp rất lớn vào kiến thức và sự hiểu biết trong giải phẫu học, thiên văn học, xây dựng dân dụng, quang học và nghiên cứu về thủy lực. Những sản phẩm lưu lại trong cuộc đời ông chỉ còn lại vài bức hoạ, cùng với một vài quyển sổ nháp tay (rơi vãi trong nhiều bộ sưu tập khác nhau các sáng tác của ông), bên trong chứa đựng các ký hoạ, minh hoạ về khoa học và bút ký.";
                txtcongtrinh.Text = "Đa số các tác phẩm của ông thuộc về lĩnh vực hội họa và khoa học kĩ thuật";
                hinhanh.Image = Math_V1._1.Properties.Resources.leo;
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Maytinh fr = new Maytinh();
            fr.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            int Numrd;
            Random rd = new Random();
            Numrd = rd.Next(1, 4);//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 10  
            if (Numrd== 1)
            {
                Duongthang fr = new Duongthang();
                fr.ShowDialog();
            }
            if (Numrd == 2)
            {
                Duongtron fr = new Duongtron();
                fr.ShowDialog();
            }
            if (Numrd == 3)
            {
                DuongElip fr = new DuongElip();
                fr.ShowDialog();
            }
            if (Numrd == 4)
            {
                Thuoctinhhinhhoc fr = new Thuoctinhhinhhoc();
                fr.ShowDialog();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Main fr = new Main();
            fr.ShowDialog();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Thongbao fr = new Thongbao("Sorry, tớ không có tài khoản Twitter");
            fr.ShowDialog();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
           
            int Numrd1;
            Random rd = new Random();
            Numrd1 = rd.Next(1, 6);//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 10  
            if (Numrd1 == 1)
            {
                tho.Text = "Có một lần thầy dạy toán làm thơ" + Environment.NewLine + "Bài thơ ấy bây giờ đang dang dở" + Environment.NewLine + "Nhưng câu thơ ý tình vẫn bỡ ngỡ" + Environment.NewLine + "Còn khô khan như môn toán của thầy" + Environment.NewLine + "" + Environment.NewLine + "Trong bài thơ thầy cộng gió với mây" + Environment.NewLine + "Bằng công thức tính Cô tang của góc" + Environment.NewLine + "Khi lá thu bay vào trong lớp học" + Environment.NewLine + "Thầy bảo rằng 'lá có lực hướng tâm'" + Environment.NewLine + "" + Environment.NewLine + "Rồi một lần mưa nhè nhẹ bâng khuâng" + Environment.NewLine + "Thầy ngẫu hứng đọc câu thơ thầy viết" + Environment.NewLine + "Thơ của thầy vô tận như số Pi";
            }
            if (Numrd1 == 2)
            {
                tho.Text = "Đường vào tim em sao quá là rắc rối";
                tho.Text += Environment.NewLine + "Không hàm số nào vẽ nổi đường đi";
                tho.Text += Environment.NewLine + "Dài vô tận như số Pi hoàn hảo";
                tho.Text += Environment.NewLine + "Dù cố mấy anh vẫn không đi hết!";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Em nhìn anh như phép chia không hết";
                tho.Text += Environment.NewLine + "Số dư dài vương vấn mãi tim anh";
                tho.Text += Environment.NewLine + "Em cứ tạo hai đường thẳng song song";
                tho.Text += Environment.NewLine + "Anh muốn gần phải bẻ cong định lí!";
                tho.Text += Environment.NewLine + "Em lặng im, không nói ra ý nghĩ";
                tho.Text += Environment.NewLine + "Anh biết rằng mình như Cos 90";
                tho.Text += Environment.NewLine + "Và tình phí đã qua dương vô cực";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Rồi ngày tròn như đường tròn lượng giác ";
                tho.Text += Environment.NewLine + "Khi bất ngờ một bài toán bậc 2 ";
                tho.Text += Environment.NewLine + "Lầm tưởng rằng nghiệm duy nhất với ai";
                tho.Text += Environment.NewLine + "Thật kinh hoàng khi phương trình vô nghiệm";
            }
            if (Numrd1 == 3)
            {
                tho.Text = "Tôi vẫn nhớ những khi em Ðối Diện";
                tho.Text += Environment.NewLine + "Ánh mắt nhìn bằng Góc Ðộ Ðường Cong";
                tho.Text += Environment.NewLine + "Lòng xôn xao cho Quĩ Ðạo đi vòng";
                tho.Text += Environment.NewLine + "Hồn tôi để Giao em Ðường Tiếp Tuyến";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Em lướt qua, cho buồn-vui Nghịch Biến ";

                tho.Text += Environment.NewLine + "Gặp một lần, nơi Tiếp Ðiểm mà thôi";
                tho.Text += Environment.NewLine + "Tôi Xoay Tròn, tìm lại nhưng xa rồi";
                tho.Text += Environment.NewLine + "Em sẽ mãi ra đi về Vô Cực";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Nhưng tình tôi là một đường Trung Trực";
                tho.Text += Environment.NewLine + "Như thật thà Cân Xứng nơi con tim";
                tho.Text += Environment.NewLine + "Tôi Phân Ðều, và xuyên qua giữa em";
                tho.Text += Environment.NewLine + "Trung Ðiểm giữa, tôi muốn tình Vuông vẹn";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Rồi một ngày, tình Tam Giác cũng đến";
                tho.Text += Environment.NewLine + "Tôi hiện hình, trong ba Góc Bù Nhau";
                tho.Text += Environment.NewLine + "Em vì ai mà Phụ để tôi sầu";
                tho.Text += Environment.NewLine + "Nhìn đau đớn Cạnh Huyền em nối mãi";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Tôi thả đời theo Trung Tuyến phóng túng";
                tho.Text += Environment.NewLine + "Em lại tìm Hình Thông Số Bình Phương";
                tho.Text += Environment.NewLine + "Ðến Nội Tâm, tôi dừng chốn đau thương";
                tho.Text += Environment.NewLine + "Buồn man mát, em đùa trên Ngoại Tiếp";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Nói làm chi, Ðịnh Phân đà muôn kiếp";
                tho.Text += Environment.NewLine + "Em lạc vào một Quĩ Tích cuồng quay";
                tho.Text += Environment.NewLine + "Tôi đứng đó, Khoảng Cách không đổi thay";
                tho.Text += Environment.NewLine + "Nhìn thầm lặng, một Góc đời của tôi";

            }
            if (Numrd1 == 4)
            {
                tho.Text = "Khi ánh xạ cuộc đời anh bình tỉnh,";
                tho.Text += Environment.NewLine + "Phép dời hình đưa anh đến với em";
                tho.Text += Environment.NewLine + "Qua lang thang cả trăm nghìn toạ độ";
                tho.Text += Environment.NewLine + "Phát hiện em ẩn hình trong số mũ";
                tho.Text += Environment.NewLine + "Phép khai căn em biến hoá khôn lường";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Ôi cuộc đời đâu như dạng toàn phương";
                tho.Text += Environment.NewLine + "Bao kỳ vọng cho khát khao tiến tới";
                tho.Text += Environment.NewLine + "Bao biến số cho một đời nông nổi";
                tho.Text += Environment.NewLine + "Phép nội suy từ chối mọi lối mòn";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Có lúc gần còn chút Epsilon ";
                tho.Text += Environment.NewLine + "Em bỗng xa như một hàm gián đoạn";
                tho.Text += Environment.NewLine + "Anh muốn thả hồn mình qua giới hạn";
                tho.Text += Environment.NewLine + "Lại chìm vơi cạn mãi giữa phương trình";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Khi tình yêu chứng minh là không thể";
                tho.Text += Environment.NewLine + "Hai tiên đề lại chênh vênh xa lạ";
                tho.Text += Environment.NewLine + "Bao lô gic như giận hờn dập xoá";
                tho.Text += Environment.NewLine + "Vẫn hiện lên một đáp số cuối cùng";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Mẫu số niềm tin đâu dễ quy đồng";
                tho.Text += Environment.NewLine + "Phép chiếu tình yêu nhiều khi đổi hướng";
                tho.Text += Environment.NewLine + "Lời giải đẹp đôi lúc do lầm tưởng";
                tho.Text += Environment.NewLine + "Ôi! Khó thay khi cuộc sống đa chiều";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Bao chu kỳ, bao đợt sóng tình yêu";
                tho.Text += Environment.NewLine + "Anh khắc khoải cơn thuỷ triều cực đại";
                tho.Text += Environment.NewLine + "Em vẫn đó bờ nguyên hàm khờ dại";
                tho.Text += Environment.NewLine + "Em mãi mãi là hằng số vô biên";            
            }
            if (Numrd1 == 5)
            {
                tho.Text = "Mỗi chúng ta là một miền xác định";
                tho.Text += Environment.NewLine + "Sống trên đời như một số tự nhiên";
                tho.Text += Environment.NewLine + "Và lắm khi đường thẳng lại hóa xiên";
                tho.Text += Environment.NewLine + "Tình ta đó biến thiên theo hàm số...";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Đời là thế như một câu tính đố!";
                tho.Text += Environment.NewLine + "Nhíu cau mày lắm lúc nghĩ không ra";
                tho.Text += Environment.NewLine + "Lòng chúng ta là một góc Alpha";
                tho.Text += Environment.NewLine + "Tan, Sin, Cos không thể nào tính được...";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Ta muốn kẻ lòng ta trên cây thước";
                tho.Text += Environment.NewLine + "Chẳng hiểu sao nó lại hóa đường cong";
                tho.Text += Environment.NewLine + "Đạo hàm đó nó vẫn cứ y nguyên";
                tho.Text += Environment.NewLine + "Đã lồi lõm, lại còn thêm điểm uốn...";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Đường biểu diễn chính là đường tình yêu";
                tho.Text += Environment.NewLine + "Luôn tăng dần lên tận maximum";
                tho.Text += Environment.NewLine + "Còn lòng ta tiến về phía vô cùng";
                tho.Text += Environment.NewLine + "Và nơi đó chính là âm vô cực...";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Là phân số ta ngỡ rằng số thực";
                tho.Text += Environment.NewLine + "Mẫu ước mơ, tử số chính là ta";
                tho.Text += Environment.NewLine + "Ứơc mơ nhiều phân số lại nhỏ đi";
                tho.Text += Environment.NewLine + "Và lắm khi ta chỉ còn bé xíu...";
            }
            if (Numrd1 == 6)
            {
                tho.Text =  "Nơi anh đến là không gian vô tận";
                tho.Text += Environment.NewLine + "Từ một điểm vẽ ra rất nhiều đường";
                tho.Text += Environment.NewLine + "Nhiều đường thẳng song song chẳng cùng chiều";
                tho.Text += Environment.NewLine + "Nhiều mặt phẳng gặp nhau nơi vô định";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Nơi quĩ tích là những đường đã định";
                tho.Text += Environment.NewLine + "Như thái dương có tâm điểm mặt trời";
                tho.Text += Environment.NewLine + "Khi trái đất luôn xoay quanh rã rời";
                tho.Text += Environment.NewLine + "Vẫn phải giữ đường xoay trong quỹ đạo";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Đường anh đi bất biến do đào tạo";
                tho.Text += Environment.NewLine + "Rất thẳng ngay nên không có đạo hàm";
                tho.Text += Environment.NewLine + "Vì đường thẳng nên dễ tính nguyên hàm ";
                tho.Text += Environment.NewLine + "Còn ẩn số thì không cần phải kiếm";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Tình yêu của anh giống như hằng số";
                tho.Text += Environment.NewLine + "Chẳng mập mờ như căn số phải tìm";
                tho.Text += Environment.NewLine + "Nghiệm thực tình vui, nghiệm ảo nát tim";
                tho.Text += Environment.NewLine + "Là hằng số tình anh không biến đổi";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Bởi em thích tình yêu trong dời đổi";
                tho.Text += Environment.NewLine + "Phép vi phân em chẻ nhỏ tình yêu";
                tho.Text += Environment.NewLine + "Dùng vị tự em đổi tình ngược chiều";
                tho.Text += Environment.NewLine + "Hằng số tiêu và tình yêu vô nghiệm";
                tho.Text += Environment.NewLine + "";
                tho.Text += Environment.NewLine + "Em yêu ơi! Tình đâu cần phải kiếm";
                tho.Text += Environment.NewLine + "Ngay trong em đã có một trái tim";
                tho.Text += Environment.NewLine + "Tình trong tim, sao cứ mãi kiếm tìm";
                tho.Text += Environment.NewLine + "Bằng toán học, sao tìm ra đáp số ???";
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Thongbao fr = new Thongbao("Anh yêu em như tình yêu toán học" + Environment.NewLine + "Như trục hoành luôn vuông góc trục tung");
            fr.ShowDialog();
        }

        private void dieuchinh_Click(object sender, EventArgs e)
        {
            if (label1.Text == "1")
            {
                label1.Text = "2";
                dieuchinh.Image = Math_V1._1.Properties.Resources.Play_32;
                timer2.Stop();
                timer3.Stop();
            }
            else
            {
                label1.Text = "1";
                timer2.Start();
                timer3.Start();
                dieuchinh.Image = Math_V1._1.Properties.Resources.Stop_32;

            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start("https://www.facebook.com/groups/laptrinh.IT/");
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start("http://laptrinhvb.net/");
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thongbao fr = new Thongbao("Hãy liên lạc với tôi để nhanh chóng thực hiện điều đó");
            fr.ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thongbao fr = new Thongbao("Hãy liên lạc với tôi để nhanh chóng thực hiện điều đó");
            fr.ShowDialog();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Huongdansudung fr = new Huongdansudung();
            fr.ShowDialog();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            FishForm fr2 = new FishForm();
            fr2.Show();
           
        }

       

       
    }
}