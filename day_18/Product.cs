using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_18
{

    /*
     * 1. Product 클래스 정의
     * 필드(속성): 제품명 (Name), 가격 (Price), 카테고리 (Category)
     * 생성자: 3개의 값 받아서 필드(속성) 초기화
     * ToString() 메서드 오버라이드
     * “제품명 / 가격 / 카테고리“ 형식으로 출력되도록 작성.
     */
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }

        public Product(string name, int price, string category)
        {
            this.Name = name;
            this.Price = price;
            this.Category = category;
        }

        // 클래스의 기본 ToString()은 클래스 이름만 반환하므로 원하는 형태로 보이게 하려면 override 해야함.
        public override string ToString()
        {
            return $"{Name} / {Price}원 / {Category}";
        }
    }

    /*
     * 2. ProductFilter 클래스 정의
     * 델리게이트 정의: ProductCondition
     * Product를 입력받고 bool을 반환하는 함수 형식
     * 정적 메서드 정의:
     * 조건을 만족하는 제품만 새 리스트에 담아 반환.
     * (Hint) public static [반환형] [메소드명]([매개변수1, 매개변수2])
     * 매개변수 1: 전체 제품 목록
     * 매개변수 2: 람다식 (조건)
     */

    public delegate bool ProductCondition(Product product);

    public class ProductFilter
    {
        /*
         * static: 객체를 만들지 않고 클래스 이름으로 직접 호출 가능
         * List<Product>: 이 Filter 메서드는 Product 리스트를 반환함
         * Filter
         * - List<Product> product: 전체 제품 목록 (필터링 대상)
         * - ProductCondition condition: 조건 함수 (델리게이트로 전달)
         * 제품 리스트 중 조건을 만족하는 항목만 필터링해서 반환하는 메서드
         */
        public static List<Product> Filter(List<Product> products, ProductCondition condition)
        {
            // 조건에 맞는 상품을 저장할 Product 목록을 미리 생성
            List<Product> result = new List<Product>();
            foreach (var product in products)
            {
                // 델리게이트에 담긴 조건 함수를 실행하여 
                // 조건을 만족하면 아래 코드를 실행
                if (condition(product))
                {
                    result.Add(product);
                }
            }
            return result;
        }
    }
}
