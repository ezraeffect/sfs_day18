using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>
            {
                new Product("의자", 50000, "가구"),
                new Product("책상", 120000, "가구"),
                new Product("노트북", 1500000, "전자"),
                new Product("커피머신", 90000, "가전"),
                new Product("소파", 300000, "가구"),
                new Product("키보드", 40000, "전자")
            };
            /*
             * 컬렉션에 객체를 직접 생성하면서 내부에 값을 동시에 넣는 형식
             * List<T> : 제네릭 컬렉션 -> 클래스
             * 클래스면? 참조 형식 -> 힙 메모리 영역에 저장
             * 리스트 안에는 [0], [1] 등 인덱스마다 주소값을 가르킴 (참조)
             * 각 주소는 실제 Product 객체가 저장된 힙 메모리의 위치(주소)를 가르침
             */
            Console.WriteLine("[100,000원 이상 제품]");

            /*
             * Product 객체를 하나 받아서, 조건으로 평가하는 익명 함수
             * ProductFilter.Filter 메서드에 조건 전달 (p는 그저 매개변수)
             * 조건: 제품 가격이 10만 원 이상인 경우만 true
             */
            List<Product> expenciveProducts = ProductFilter.Filter(products, p => p.Price >= 100000);

            foreach (Product product in expenciveProducts)
            {
                Console.WriteLine($"- {product}");
                // C#에서는 문자열 보간 방법이나, 문자열 연결시 객체를 문자열처럼 넣으면 ToString 자동 호출
            }

            Console.WriteLine("[가구 카테고리 제품]");

            List<Product> furnitureProducts = ProductFilter.Filter(products, p => p.Category == "가구");

            foreach (Product product in furnitureProducts)
            {
                Console.WriteLine($"- {product}");
            }
        }
    }
}
