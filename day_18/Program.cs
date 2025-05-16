using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_18
{

    /*
    * Delegate (= 대리자) 메서드를 대신 실행해줌.
    * 메서드에 대한 *참조*를 저장할 수 있는 형식(타입)
    * 즉, 메서드를 직접 실행하지 않고, 실행 방법만 담아두는 일종의 *포인터 함수* 역할.
    * 
    * 1. 메서드를 나중에 실행하고 싶을 때
    * - 실행 로직을 저장해뒀다가 특정 조건이 되면 실행.
    * 2. 메서드를 매개변수로 전달하고 싶을 때
    * 3. 이벤트 시스템을 만들 때
    * 
    * 왜 씀?
    * 1. 타입 안정성
    * - 컴파일 시점에 타입 검사. (매개변수, 반환값이 동일해야 함)
    * 2. 여러 메서드 연결 가능 += 연산자로 메서드 체이닝 가능 (= Multicast Delegate)
    * 3. 이벤트와 잘 어울림. 
    * - C# 이벤트는 사실상 델리게이트 기반이다.
    * 
    * [접근제한자] delegate [반환형] [델리게이트 명] ([매개변수 목록]);
    * namespace 내부, class 외부 (type이기 때문에)
    */

    // delegate 기본 사용법
    // 1-1. delegate 선언 (사용자 정의 타입)
    public delegate void MyDelegate();
    // 반환값이 void이며 매개변수가 없는 메소드를 참조할 수 있는 타입 MyDelegate를 선언.
    // 이런 형태의 메서드를 받을 수 있습니다. <= 라고 약속하는 타입 정의.

    /*
     * Multicast delegate
     * 하나의 델리게이트에 여러 개의 메서드를 연결해서 순차적으로 실행하는 기능.
     * += : 메서드 연결
     * -= : 메서드 제거
     * 
     * retrun 값이 있는 델리게이트에는 일반적으로 Multicast 사용 X
     * 마지막 값만 사용되므로 혼란 가능성 있음.
     */
    public delegate void NofityDelegate();

    /*
     * 익명 메서드
     * 메서드를 따로 만들지 않고 델리게이트에 직접 정의하는 것이 특징
     * 
     * 왜 씀?
     * 1. 한 번만 쓸 메서드
     * - 굳이 이름 붙여서 만들 필요 없음
     * 2. 로컬 함수처럼 간단한 로직
     * - 코드 흐름을 방해하지 않고 인라인으로 처리 가능
     * 3. 델리게이트에 직접 연결 할 때
     * - delegate() { ... } 로 간결하게 사용 가능
     * 
     * [델리게이트 타입] 변수 = delegate([매개변수]) {실행할코드};
     */
    public delegate void ActionDelegate();

    /*
     * 람다식 델리게이트
     * - 메서드를 이름 없이 간결하게 표현하는 문법
     * - delegate 키워드 없이 () => {} 형식으로 작성
     * - 함수 자체를 값처럼 전달
     * - 변수에 저장하거나 다른 메서드의 매개변수로 전달 할 수 있음
     * - 코드 한 줄, 여러 줄 모두 가능
     * 
     * 왜 씀?
     * 1. 간결함
     * - delegate {} 보다 훨씬 짧음
     * 2. 실무에서 자주 사용
     * 
     * [델리게이트 타입] [변수명] = [매개변수] => [실행 코드];
     */
    public delegate void CalcDelegate(int a, int b);


    internal class Program
    {


        // 1-2. 메소드 정의
        public static void SayHello() => Console.WriteLine("안녕하세요");
        // 반환형 void, 매개변수 없음 => MyDelegate와 형태가 알맞음.
        // MyDelegate에 저장이 가능

        public static void SoundAlarm()
        {
            Console.WriteLine("경보음이 울립니다");
        }

        public static void FlashLight()
        {
            Console.WriteLine("경고등이 깜빡입니다.");
        }

        public static void SendNotification()
        {
            Console.WriteLine("관리자에게 알림을 전송합니다.");
        }

        static void Main(string[] args)
        {
            // 1-3. 델리게이트 객체 생셩 & 메서드 참조.
            // 1) 명시적 방식
            MyDelegate del = new MyDelegate(SayHello);
            // SayHello 메서드를 MyDelegate 타입 변수인 del에 할당 = 메서드 저장

            // 2) 암묵적 방식
            MyDelegate del2 = SayHello;

            del(); // 메서드 호출
            del2();

            // 2-3. 델리게이트 변수 선언 + 메서드 연결
            NofityDelegate mulDel = SoundAlarm;
            mulDel += FlashLight;
            mulDel += SendNotification;

            Console.WriteLine("========== 메서드 실행 ===========");
            mulDel(); // 세 개의 메서드가 순차적으로 실행됨.

            // 특정 메서드 제거
            Console.WriteLine("========== FlashLight 제거 후 실행 ===========");
            mulDel -= FlashLight;
            mulDel();

            // 3-2. 익명 메서드로 델리게이트 정의
            ActionDelegate acDel = delegate ()
            { Console.WriteLine("익명 메서드입니다."); };

            acDel();

            /*
             * 4. 내장 델리게이트 타입 & 익명 메서드
             * C#에서 기본적으로 제공하는 델리게이트
             * 자주 쓰는 함수 형태(매개변수 + 반환 값)을 위해 특정 표준 델리게이트 타입을 미리 정의 해 놓음.
             * 
             * 왜 씀?
             * 1. 간결하고 범용적인 함수 형태를 빠르게 표현 할 수 있어서
             * 
             * 언제 씀?
             * 1. 간단한 함수, 반복적으로 많이 쓰이는 형태라면 내장 델리케이트 타입
             * 2. 의미 있는 이름을 붙이거나 복잡한 구조로 사용자 정의를 원한다면 직접 delegate 선언
             * 
             * 종류
             * Func<...>
             * - 반환값 O, 반드시 마지막 타입이 반환형
             * Action<...>
             * - 반환값 X, Void
             * Predicate<...>
             * - bool 반환하는 특수한 Func (조건 필터용)
             * 
             * Func<T1, T2, ... , TResult>
             * - 앞쪽은 매개변수 타입들
             * - 마지막은 반환(return) 타입.
             */

            Func<int, int, int> addDel = delegate(int a, int b) { return a + b; };
            // int형 변수 2개를 매개변수로 하고 int로 반환하는 메서드만 델리케이트에 추가 가능하다
            int result = addDel(1, 2);
            Console.WriteLine(result.ToString());

            Action<string> printMessage = delegate(string s) { Console.WriteLine(s); };
            printMessage("메세지입니다.");

            // 기본 람다식 사용
            CalcDelegate printSum = (x, y) =>
            {
                int sum = x + y;
                Console.WriteLine(sum.ToString());
            };
            printSum(10, 20);

            // Func 사용 - 반환 값이 있는 람다
            Func<int, int, int> multiply = (x, y) =>  x * y;
            int result2 = multiply(1, 2); // 람다식은 값처럼 변수에 저장 가능
            Console.WriteLine(result2.ToString());

            // 일반적인 메서드
            int multiply2(int a, int b)
            {
                return a + b;
            }
            int result3 = multiply2(10, 20);
            Console.WriteLine(result3.ToString());

            // Action 사용 - void 반환 람다
            Action<string> callName = name => Console.WriteLine($"{name}님 안녕하세요");
            callName("Damon");
        }
    }
}
