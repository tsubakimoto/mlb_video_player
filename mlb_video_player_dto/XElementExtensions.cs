using System.Xml.Linq;

namespace mlb_video_player_dto
{
    public static class XElementExtensions
    {
        #region 拡張メソッド

        /// <summary>
        /// XElement オブジェクトに指定した属性があるかどうかを示します。
        /// </summary>
        /// <param name="e">XElement オブジェクト</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static bool HasAttribute(this XElement e, string attrName)
        {
            return e.Attribute(attrName) != null;
        }

        /// <summary>
        /// XElement オブジェクトの指定した属性が指定した値であるかどうかを示します。
        /// </summary>
        /// <param name="e">XElement オブジェクト</param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性値</param>
        /// <returns></returns>
        public static bool HasAttribute(this XElement e, string attrName, string attrValue)
        {
            return e.HasAttribute(attrName) && e.Attribute(attrName).Value == attrValue;
        }

        /// <summary>
        /// XElement オブジェクトの指定した属性が指定した値を含んでいるかどうかを示します。
        /// </summary>
        /// <param name="e">XElement オブジェクト</param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性値</param>
        /// <returns></returns>
        public static bool ContainsAttribute(this XElement e, string attrName, string attrValue)
        {
            return e.HasAttribute(attrName) && e.Attribute(attrName).Value.Contains(attrValue);
        }

        #endregion
    }
}
