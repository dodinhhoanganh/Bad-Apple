using System; //Thư viện chính
using System.Diagnostics; //Thư viện để sử dụng stopwatch
using System.IO; //Đọc File
using NAudio.Wave; //Thư viện audio

namespace BadApple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear(); //Clear console (cho sạch sẽ)
            Console.WriteLine("\n-------------------BAD APPLE---------------------\n"); //Viết mấy câu giới thiệu
            Console.WriteLine("\n-----------------Create by Rimi------------------\n");
            Console.WriteLine("\nPress any key to continue...\n"); //Và dừng chương trình tới khi người dùng bấm 1 bút nào đó
            Console.ReadKey(); //readkey là nhận tín hiệu từ người dùng khi bấm phím nào đó, nếu người dùng nhấn phím thì tiếp tục
            Console.WindowWidth = 120; //set chiều dài
            Console.WindowHeight = 50; //set chiều cao
            Console.CursorVisible = false; //tắt con trỏ trong console
            Mp3FileReader mp3Reader = new Mp3FileReader(@".\badapple\BA.mp3"); //Cái này trong NAudio, để đọc file mp3 từ ổ cứng
            WaveOut waveOut = new WaveOut(); //Sau đó sử dụng wav out
            waveOut.Init(mp3Reader);//init nó vs waveout (có thể coi wav out như là 1 cái để liên kết file mp3 vs loa)
            waveOut.Play(); //sau đó play
            int i = 0; //init 2 biến chính là i
            double tpf = 6.95017454776; //time per frame (Thời gian cho mỗi khung hình), dùng double vì có thể là số thập phân (Nó sẽ = 1000/fps) ở đây là xấp xỉ 6.95017454776 vì ở đây là 144fps
            double count_t = 0; //Đếm thời gian, dùng double là ổn nhất vì đây là số thập phân
            Stopwatch st = new Stopwatch(); //tạo stopwatch, rất quan trọng vì đó là thời gian chạy chương trình
            st.Start(); //chạy stopwatch
            Console.SetCursorPosition(0, 1);
            while (i <= 31510) { //vòng while chạy tới 31510 vì có tổng cộng 31511 frames, tính từ 0
                if (st.ElapsedMilliseconds - count_t >= tpf) //Nếu thời gian chạy chương trình theo ms - thời gian đếm được >= time per frame thì in file ra màn hình
                {
                    Console.WriteLine(File.ReadAllText(@".\badapple\ASCII-BA" + i + ".txt")); //Load file và in ra màn hình
                    Console.WriteLine("\n-------------------------\n"); //In phần cách
                    Console.WriteLine("Frame: " + i); //In số frame đã load (là biến i)
                    Console.WriteLine("FPS: " + Math.Round((decimal)1000/(decimal)tpf)); //In số FPS (FPS = 1000/tpf)
                    Console.WriteLine("TPF: " + tpf); //In ra time per frame (Để debug là chính)
                    int Min = st.Elapsed.Minutes; //số phút đã chạy chương trình
                    int sec = st.Elapsed.Seconds; //Số giây đã chạy
                    string min = string.Empty; //chuyển qua dạng string
                    string Sec = string.Empty; //chuyển qua dạng string
                    min = "0" + Min; //Lúc nào cx + thêm 0 vào min vì video dài dưới 10p, nếu dài hơn 10p thì phải dùng thêm lệnh if
                    if (sec < 10) //Nếu second nhỏ hơn 10 thì + thêm 0 vào
                    {
                        Sec = "0" + sec;
                    }
                    else
                    {
                        Sec = sec.ToString(); //Nếu ko thì cứ in second ra
                    }
                    Console.WriteLine("Time Elapsed: {0}:{1}", min, Sec); //In ra time, ở trên chỉ là format lại time cho dễ nhìn
                    Console.WriteLine("\n-------------------------\n"); //In phần cách
                    Console.SetCursorPosition(0, 1); //Set con trỏ về x=0; y=1 để tiếp tục in frame tiếp theo
                    i++; //+ vào biến i 1 đơn vị
                    count_t += tpf; //+ thêm vào thời gian thực hiện time per frame, có nghĩa là khi load 1 frame, thì biến count_t = count_t + tpf
                }
            }
            Console.Clear(); //Sau khi hoàn thành thì làm sạch console
            Console.WriteLine("END."); //Sau đó in ra chữ end cho mn biết
            st.Stop(); //Dừng stopwatch lại vì ko còn j để chạy
            Console.CursorVisible = true; //Cho hiện lại con trỏ
            Console.ReadKey(); //Dừng chương trình
        }
    }
}
