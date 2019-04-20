namespace FlatBuffers
{
    public static class FlatBufferHelper
    {
        /// <summary>
        /// 添加数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="builder">构造类</param>
        /// <param name="datas">数组数据</param>
        /// <returns></returns>
        public static VectorOffset BuildVector<T>(this FlatBufferBuilder builder, T[] datas) where T : struct
        {
            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            builder.StartVector(size, datas.Length, size);
            builder.Add(datas);
            return builder.EndVector();
        }

        /// <summary>
        /// 创建字符串对象
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StringOffset BuildString(this FlatBufferBuilder builder, string data)
        {
            return data == null ? default(StringOffset) : builder.CreateString(data);
        }

        /// <summary>
        /// 解码数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static T[] DecodeVector<T>(this FlatPLsModelData data, string fieldName)
        {
            //数组长度
            var prop = typeof(FlatPLsModelData).GetProperty(fieldName + "Length");
            int len = (int)prop.GetValue(data);
            if (len == 0)
                return null;

            var method = typeof(FlatPLsModelData).GetMethod(fieldName);

            T[] retData = new T[len];

            for (int i = 0; i < len; i++)
            {
                object[] paras = new object[] { i };
                retData[i] = (T)method.Invoke(data, paras);
            }

            return retData;
        }
    }
}
